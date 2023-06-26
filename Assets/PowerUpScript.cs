using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{

    [Range(0, 3), SerializeField] private int powerUpType = 0;
    [Range(0f, 2f), SerializeField] private float powerTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
     *  0 means Extra Damage, and Speed Up
     *  1 means Shield
     *  2 means Unlimited Ammo
     *  3 means Extra Speed
     */
    public int GetPType()
    {
        return powerUpType;
    }

    public float GetPTime()
    {
        return powerTime;
    }
}
