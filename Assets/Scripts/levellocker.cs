using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levellocker : MonoBehaviour
{
    public Button[] lvlbuttons;
    // Start is called before the first frame update
    void Start()
    {
        int LevelAt = PlayerPrefs.GetInt("LevelAt", 1);
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        for (int i = 0;  i < lvlbuttons.Length; i++)
        {
            if (i + 1 > LevelAt)
            {
                lvlbuttons[i].interactable = false;
            }
        }
         
    }

    
}
