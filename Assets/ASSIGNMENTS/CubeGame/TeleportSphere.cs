using UnityEngine;

public class TeleportSphere : MonoBehaviour
{
    public GameObject sphere;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sphere.transform.position.y <= -3)
        {
            sphere.transform.position = new Vector3(1.2f, 2.27f, 1.5f);
        }
    }
}
