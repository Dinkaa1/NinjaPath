using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoice : MonoBehaviour
{
    public AudioSource sound;
    public void Load1()
    {
        sound.Play();
        StartCoroutine(LoadLevelAfterDelay(sound.clip.length, 12));
    }

    public void Load2()
    {
        sound.Play();
        StartCoroutine(LoadLevelAfterDelay(sound.clip.length, 13));
    }

    public void Load3()
    {
        sound.Play();
        StartCoroutine(LoadLevelAfterDelay(sound.clip.length, 14));
    }

    public void Load4()
    {
        sound.Play();
        StartCoroutine(LoadLevelAfterDelay(sound.clip.length, 15));
    }

    public void LoadMenu()
    {
        PlayerPrefs.GetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        sound.Play();
        StartCoroutine(LoadLevelAfterDelay(sound.clip.length, 0));
    }

    IEnumerator LoadLevelAfterDelay(float delay, int num)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(num);
    }
}
