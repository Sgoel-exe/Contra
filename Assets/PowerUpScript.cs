using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [Tooltip("0-Damage,1-Speed,2-Ammo,3-Shield")]
    [Range(0, 3), SerializeField] private int powerUpType = 0;
    [Range(0f, 5f), SerializeField] private float powerTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
     *  0 means Extra Damage, and Speed Up
     *  1 means FlashMode
     *  2 means Unlimited Ammo
     *  3 means Shield
     */
    public int GetPType()
    {
        return powerUpType;
    }

    public float GetPTime()
    {
        return powerTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Die());
            Manager(collision);
        }
    }

    private void Manager(Collider2D collison)
    {
        switch (powerUpType)
        {
            case 0: StartCoroutine(DamagePower(collison)); break;
            case 1: StartCoroutine(FlashMode(collison)); break;
            case 2: StartCoroutine(Ammo(collison)); break;
            case 3: Shield(collison); break;
            default: break;
        }
    }

    private IEnumerator DamagePower(Collider2D collison)
    {
        ShootMechanics PlShoot = collison.GetComponent<ShootMechanics>();
        int ogDamage = PlShoot.gerBulletDamge();
        float ogSpeed = PlShoot.getBulletSpeed();
        Debug.Log("Damage");
        PlShoot.setBulletDamage(100);
        PlShoot.setBulletSpeed(40f);
        yield return new WaitForSeconds(powerTime);
        PlShoot.setBulletSpeed(ogSpeed);
        PlShoot.setBulletDamage(ogDamage);
        yield break;
    }

    private IEnumerator FlashMode(Collider2D collison)
    {
        Movement PlMove = collison.gameObject.GetComponent<Movement>();
        Debug.Log("FlashMode");
        PlMove.setSpeedMultiplier(10f);
        yield return new WaitForSeconds(powerTime);
        PlMove.setSpeedMultiplier(1f);
        yield break;
    }

    private IEnumerator Ammo(Collider2D collison)
    {
        ShootMechanics PlShoot = collison.GetComponent<ShootMechanics>();
        PlShoot.setInfinite(true);
        yield return new WaitForSeconds(powerTime);
        PlShoot.setInfinite(false);
        yield break;
    }

    private void Shield(Collider2D collison)
    {
        HealthScript PlHealth = collison.GetComponent<HealthScript>();
        StartCoroutine(PlHealth.Shield(powerTime));
    }



    private IEnumerator Die()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(powerTime*1.5f);
        Destroy(gameObject);
    }
}
