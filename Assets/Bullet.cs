using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 15f;
    public int damage = 50;
    public float lifeTime = 1.5f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    public void BulletCollison()
    {
        Destroy(gameObject);
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
