using UnityEngine;

public class BetterGameManager : MonoBehaviour
{
    
    public GameObject MainMenu;

    public GameObject PauseMenu;

    public GameObject GameUI;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMenu.activeInHierarchy)
        {
            GameUI.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (PauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
