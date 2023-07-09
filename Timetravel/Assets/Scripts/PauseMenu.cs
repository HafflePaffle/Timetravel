using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused = false;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused == false)
            {
                Pause();
                
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
