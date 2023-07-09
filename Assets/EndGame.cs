using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(end());
                
            }
        }
    }

    public IEnumerator end()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Level", 1);
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("End");
    }
}
