using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontestroyAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("BackMusic");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(transform.gameObject);
        
    }

    public void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "WelcomingMenu" && currentScene.name != "LevelMenu")
        {
            Destroy(this.gameObject);
        }
    }
}
