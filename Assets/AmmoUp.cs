using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int num = (int)Random.Range(3f, 8f);
            Debug.Log(num);
            ShootMechanics ammoAdder = collision.gameObject.GetComponent<ShootMechanics>();
            ammoAdder.addAmmo(num);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
