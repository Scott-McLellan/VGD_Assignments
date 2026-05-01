using UnityEngine;

public class PlayBut : MonoBehaviour
{
    public GameObject MainMenu;

    public GameObject GameUI;
    
    public void HideMainMenu()
    {
        MainMenu.SetActive(false);
        GameUI.SetActive(true);
    }
}
