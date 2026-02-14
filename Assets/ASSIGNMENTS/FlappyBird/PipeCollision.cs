using UnityEngine;

public class PipeCollision : MonoBehaviour
{
    
    public GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Game Over");
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
