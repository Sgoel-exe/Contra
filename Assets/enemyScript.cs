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
    
    public bool canMove = true;
    public float stopTime = 1f;
    private float temptimer = 0f;
    
    //public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.setHealth(curhealth, maxhealth);
        //flip();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            flip();
        }
        else
        {
            //animator.SetBool("isShooting", true);
            temptimer += Time.deltaTime;
        }

        if (temptimer > stopTime)
        {
            //animator.SetBool("isShooting", false);
            canMove = true;
            temptimer = 0f;
            TransformFlip();
        }
        //flip();
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
            TransformFlip();
        }
       //isFacingLeft = !isFacingLeft;
       // transform.Rotate(0f, 180f, 0f);
    }

    public void TransformFlip()
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0f,180f, 0f);
    }

    public void stopMovment()
    {
        canMove = false;
        body.velocity = Vector2.zero;

    }
}
