using UnityEngine;

public class MainMenuButton : MonoBehaviour
{

    public GameObject optionsMenu;
    
    public GameObject mainMenu;
    
    public GameObject creditsMenu;

   

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
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
