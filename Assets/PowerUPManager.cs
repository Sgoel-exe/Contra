using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPManager : MonoBehaviour
{
    private int Ptype;
    private float Ptime;
    private ShootMechanics PlShoot;
    private Movement PlMove;
    private HealthScript PlHealth;
    
    void Start()
    {
        PlShoot = GetComponent<ShootMechanics>();
        PlMove = GetComponent<Movement>();
        PlHealth = GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Power"))
            {
                PowerUpScript pS = collision.gameObject.GetComponent<PowerUpScript>();
                Ptype = pS.GetPType();
                Ptime = pS.GetPTime();
                Debug.Log("Collision Detection");
                Manager();
            }
        }
    }

    private void Manager()
    {
        switch (Ptype)
        {
            case 0: StartCoroutine(DamagePower()); break;
            case 1: StartCoroutine(FlashMode()); break;
            case 2: Ammo(); break;
            case 3: Shield(); break;
            default: break;
        }
    }

    private IEnumerator FlashMode()
    {
        Debug.Log("FlashMode");
        PlMove.setSpeedMultiplier(10f);
        yield return new WaitForSeconds(Ptime);
        PlMove.setSpeedMultiplier(1f);
        yield return new WaitForSeconds(Ptime);
    }

    private void Ammo()
    {
        throw new NotImplementedException();
    }

    private void Shield()
    {
        throw new NotImplementedException();
    }

    private IEnumerator DamagePower()
    {
        int ogDamage = PlShoot.gerBulletDamge();
        float ogSpeed = PlShoot.getBulletSpeed();
        Debug.Log(ogDamage);
        PlShoot.setBulletDamage(100);
        PlShoot.setBulletSpeed(40f);
        yield return new WaitForSeconds(Ptime);
        PlShoot.setBulletSpeed(ogSpeed);
        PlShoot.setBulletDamage(ogDamage);
    }
}
