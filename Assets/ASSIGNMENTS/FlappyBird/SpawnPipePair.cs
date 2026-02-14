using UnityEngine;

public class SpawnPipePair : MonoBehaviour
{

    public float timer;
    public float timerCooldown;
    public GameObject PipePair;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timerCooldown)
        {
            timer = 0;
            Instantiate(PipePair, transform.position, Quaternion.identity);
        }
    }
}
