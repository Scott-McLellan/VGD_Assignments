using UnityEngine;

public class WinningTrigger : MonoBehaviour
{
    public GameObject text;
    
    public GameObject sphere;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
        Time.timeScale = 0;
    }
}
