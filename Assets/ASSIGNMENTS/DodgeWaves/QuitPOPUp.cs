using UnityEngine;

public class QuitPOPUp : MonoBehaviour
{
    public GameObject QuitpopUp;

    public void No()
    {
        QuitpopUp.SetActive(false);
    }

    public void ReallyQuit()
    {
        QuitpopUp.SetActive(true);
    }

    void Update()
    {
        if (QuitpopUp.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }
}
