using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public GameObject pauseMenu;

    public void Play()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
