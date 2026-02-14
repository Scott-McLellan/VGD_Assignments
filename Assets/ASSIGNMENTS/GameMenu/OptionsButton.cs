using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    
    public GameObject optionsMenu;

    public GameObject mainMenu;

    public void Options()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
        
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
