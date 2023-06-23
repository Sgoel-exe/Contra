using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float time = 3f;
    private bool isPlayerOnSpike = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthScript health = collision.gameObject.GetComponent<HealthScript>();
            isPlayerOnSpike = true;
            StartCoroutine(tickDamage(health));
            collision.rigidbody.AddForce(new Vector2(0f, 50f), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnSpike = false;
        }
    }

    private  IEnumerator tickDamage(HealthScript health)
    {
        while (isPlayerOnSpike)
        {
            health.TakeDamage(damage);
            yield return new WaitForSeconds(time);
        }
    }
}
