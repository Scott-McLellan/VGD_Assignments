using UnityEngine;

public class SpawnPipePair : MonoBehaviour
{

    public float timer;
    public float timerCooldown;
    public GameObject PipePair;

    public float maxY;
    public float minY;
    
    
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
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);
            Instantiate(PipePair, spawnPosition, Quaternion.identity);
        }
    }
}
