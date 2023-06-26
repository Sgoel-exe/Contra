using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 30f;
    public int damage = 15;
    public float lifeTime = 1.5f;
    private float timeAlive = 0;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        //Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if(timeAlive >= lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            timeAlive += Time.smoothDeltaTime;
        }
    }

    public void BulletCollison()
    {
        Destroy(gameObject);
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public void setLifeTime(float lifetime)
    {
        this.lifeTime = lifetime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        EnemyHealthScript enemy = collision.GetComponent<EnemyHealthScript>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        else if(collision.gameObject.CompareTag("Bullet"))
        {
            if(this.damage >= collision.gameObject.GetComponent<EnemyBullet>().getDamage())
            {
                collision.gameObject.GetComponent<EnemyBullet>().BulletCollison();
            }

        }
        Destroy(gameObject);
    }
}
