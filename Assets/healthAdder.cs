using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthAdder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int num = (int)Random.Range(5f, 15f);
            Debug.Log(num);
            HealthScript health = collision.gameObject.GetComponent<HealthScript>();
            health.addHealt(num);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
