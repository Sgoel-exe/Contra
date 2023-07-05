using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
    }
}
