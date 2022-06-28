using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinousMusic : MonoBehaviour
{
    public static ContinousMusic instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                instance = this;
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
