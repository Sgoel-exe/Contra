using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public int levelToLoad = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SaveStuff(collision);
                StartCoroutine(WaitAndLoadScene());
            }
        }
    }

    void SaveStuff(Collider2D collison)
    {
        collison.GetComponent<HealthScript>().saveData();
        collison.GetComponent<ShootMechanics>().saveData();
        PlayerPrefs.SetInt("Level", 2);
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + levelToLoad.ToString());
    }
}
