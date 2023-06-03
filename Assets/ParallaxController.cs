
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cameraTransform;
    Vector3 camStartPos;
    float distance;


    GameObject[] layers;
    Material[] materials;
    float[] speeds;

    float farthestLayerDistance;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        camStartPos = cameraTransform.position;

        int layerCount = transform.childCount;
        materials = new Material[layerCount];
        layers = new GameObject[layerCount];
        speeds = new float[layerCount];

        for(int i = 0; i < layerCount; i++)
        {
            layers[i] = transform.GetChild(i).gameObject;
            materials[i] = layers[i].GetComponent<Renderer>().material;
            speeds[i] = speed * (i + 1);
        }
        speedCalc(layerCount);
    }

    void speedCalc(int backCount)
    {
        for(int i = 0; i < backCount; i++)
        {
            if ((layers[i].transform.position.z - camStartPos.z) > farthestLayerDistance)
            {
                farthestLayerDistance = layers[i].transform.position.z - camStartPos.z;
            }
        }

        for(int i = 0; i < backCount; i++)
        {
            speeds[i] = 1 - (layers[i].transform.position.z - cameraTransform.position.z) / farthestLayerDistance;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        distance = cameraTransform.position.x - camStartPos.x;
        transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, 0);

        for(int i = 0; i < layers.Length; i++)
        {
            float speeed = speeds[i] * speed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speeed);
        }
    }
}
