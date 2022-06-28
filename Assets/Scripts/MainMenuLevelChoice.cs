using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLevelChoice : MonoBehaviour
{
    public AudioSource sound;
    

    public void StartFromScratch()
    {
        sound.Play();
        StartCoroutine(LoadLevelAfterDelay(sound.clip.length, 1));
    }

    public void Load()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }

    public void Quit()
    {
       Application.Quit();
    }

    IEnumerator LoadLevelAfterDelay(float delay, int num)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(num);
    }
}
