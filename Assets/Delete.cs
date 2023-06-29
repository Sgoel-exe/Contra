using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        del();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
    public void del()
    {
        PlayerPrefs.DeleteAll();
    }
}
