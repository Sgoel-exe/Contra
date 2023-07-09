using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartContra : MonoBehaviour
{
    public void NewGame()
    {
        PlayerPrefs.SetInt("Level", 1);
        LoadLevel();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Level").ToString());
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void toStart()
    {
        SceneManager.LoadScene("Start");
    }
}
