using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    public int health = 100;
    public GameObject deathEffect;
    public Rigidbody2D body;
    public float speed = 5f;
    public Transform groundCheck;
    public bool isFacingLeft = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       flip();
    }

    public void flip()
    {
        if (isFacingLeft)
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
       isFacingLeft = !isFacingLeft;
       transform.Rotate(0f, 180f, 0f);
    }

}
