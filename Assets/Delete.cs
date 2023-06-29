using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public void del()
    {
        PlayerPrefs.DeleteAll();
    }
}
