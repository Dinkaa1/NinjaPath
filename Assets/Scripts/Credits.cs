using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource sound;
    public void Quit()
    {
        sound.Play();
        StartCoroutine(QuitAfterDelay(sound.clip.length));
    }

    IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerPrefs.SetInt("LevelAt", 1);
        CompleteLevel();
    }
    
    public void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
