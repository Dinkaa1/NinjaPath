using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    // Update is called once per frame

    public AudioSource sound;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        sound.Play();
        StartCoroutine(LoadLevelAfterDelay(sound.clip.length, 0));
        GameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        sound.Play();
        StartCoroutine(QuitAfterDelay(sound.clip.length));
    }

    IEnumerator LoadLevelAfterDelay(float delay, int num)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(num);
    }

    IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
