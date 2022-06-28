using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScript : MonoBehaviour
{
    public AudioSource sound;
   

    public void StartGame()
    {
        sound.Play();
        StartCoroutine(LoadLevelAfterDelay(sound.clip.length));
    }


    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LevelMenu");
    }
}
