using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlats : MonoBehaviour
{
    private GameObject currentPlat;
    public BoxCollider2D playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(currentPlat != null)
            {
                StartCoroutine(IgnoreCollision());
            }
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "OneWayPlatforms")
        {
            currentPlat = collision.gameObject;
            //Physics2D.IgnoreCollision(playerCollider, currentPlat.GetComponent<BoxCollider2D>(), true);
        }
        
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "OneWayPlatforms")
        {
            currentPlat = null;
            //Physics2D.IgnoreCollision(playerCollider, currentPlat.GetComponent<BoxCollider2D>(), false);
        }
    }

    public IEnumerator IgnoreCollision()
    {
        BoxCollider2D platCollider = currentPlat.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platCollider, true);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platCollider, false);
    }

}
