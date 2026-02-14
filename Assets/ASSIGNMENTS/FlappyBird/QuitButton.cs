using UnityEngine;
using UnityEngine.Rendering;

public class QuitButton : MonoBehaviour
{
    public GameObject pauseMenu;

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    }
    
    #endif
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
