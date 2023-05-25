using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartContra : MonoBehaviour
{
    public void LetsGo(string level)
    {
        SceneManager.LoadScene(level);
    }
}
