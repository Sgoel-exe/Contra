using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public GameObject point;
    public GameObject DamameMaxed;
    public GameObject HealthMaxed;
    public GameObject PointUsedUp;
    public void UpDamage()
    {
        int dmg = PlayerPrefs.GetInt("Damage");
        if(point.GetComponent<JustAPoint>().point > 0)
        {
            if(dmg >= 75)
            {
                DamameMaxed.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("Damage", dmg + 10);
                point.GetComponent<JustAPoint>().point = 0;
            }
        }
        else
        {
            StartCoroutine(Warning());
        }
    }

    public void UpHealth()
    {
        int health = PlayerPrefs.GetInt("MaxH");
        if (point.GetComponent<JustAPoint>().point > 0)
        {
            if (health >= 150)
            {
                HealthMaxed.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("MaxH", health + 10);
                point.GetComponent<JustAPoint>().point = 0;
            }
        }
        else
        {
            StartCoroutine(Warning());
        }
    }

    public void nextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level"));
    }
    
    public IEnumerator Warning()
    {
        PointUsedUp.SetActive(true);
        yield return new WaitForSeconds(1f);
        PointUsedUp.SetActive(false);
    }
}
