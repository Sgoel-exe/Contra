using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    public int maxhealth = 100;
    public int curhealth = 100;
    public GameObject deathEffect;
    public Rigidbody2D body;
    public float speed = 5f;
    public Transform groundCheck;
    public bool isFacingLeft = true;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.setHealth(curhealth, maxhealth);
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
        curhealth -= damage;
        healthBar.setHealth(curhealth, maxhealth);
        if(curhealth <= 0)
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
    if (collision.gameObject.CompareTag("ground"))
        {
            isFacingLeft = !isFacingLeft;
            transform.Rotate(0f, 180f, 0f);
        }
       //isFacingLeft = !isFacingLeft;
       // transform.Rotate(0f, 180f, 0f);
    }

}
