using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    public GameObject creditsMenu;
    
    public GameObject mainMenu;

    public void OnClick()
    {
        creditsMenu.SetActive(true);
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
