using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    [SerializeField] private float speed = 4;
    private int currentPoint = 0;
    //private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[currentPoint].position) < 0.1f)
        {
            currentPoint++;
            if(currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
    
    private void OnDrawGizmos()
    {
        for(int i = 0; i < points.Length; i++)
        {
            if(i + 1 < points.Length)
            {
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
            else
            {
                Gizmos.DrawLine(points[i].position, points[0].position);
            }
        }
    }
}
