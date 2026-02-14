using Unity.VisualScripting;
using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject gameOverMenu;

    public bool isPaused;

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }
}
