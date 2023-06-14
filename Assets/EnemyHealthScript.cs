using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth = 100;
    public HealthBar healthBar;
    public GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.setHealth(curHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        healthBar.setHealth(curHealth, maxHealth);
        if (curHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
