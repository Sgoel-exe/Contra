using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPManager : MonoBehaviour
{
    private int Ptype;
    private float Ptime;
    
    void Start()
    {
        
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
                Manager();
            }
        }
    }

    private void Manager()
    {
        switch (Ptype)
        {
            case 0: DamagePower(); break;
            case 1: Shield(); break;
            case 2: Ammo(); break;
            case 3: FlashMode(); break;
            default: break;
        }
    }

    private void FlashMode()
    {
        throw new NotImplementedException();
    }

    private void Ammo()
    {
        throw new NotImplementedException();
    }

    private void Shield()
    {
        throw new NotImplementedException();
    }

    private void DamagePower()
    {
        throw new NotImplementedException();
    }
}
