using Unity.IntegerTime;
using UnityEngine;

public class SpereVerticalMovement : MonoBehaviour
{
    
    public float speed;

    public float minX;
    
    public float maxX;

    public float scale;

    public float waveTimer;
    public float waveTimerInterval = 10;

    public float waitTimer;
    
    public float waitTimerInterval = 5f;

    public int waveCount;
    
    private bool IsObjectsPaused;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        waveTimer += Time.deltaTime;
        
        scale = Random.Range(1f, 2.5f);

        if (!IsObjectsPaused)
        {
            transform.position += new Vector3(0.0f, -1.0f, 0.0f) * speed * Time.deltaTime;
        }

        if (!IsObjectsPaused && transform.position.y <= -5.5)
        {
            speed = Random.Range(1f, 4f);
            transform.position = new Vector3(Random.Range(minX, maxX), 8f, 0.0f * speed * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, 1.0f);
        }

        if (waveTimer >= waveTimerInterval)
        {
            if (!IsObjectsPaused)
            {
                IsObjectsPaused = true;
                speed = 0;
                waitTimer = 0;

                waveCount++;
                
                transform.position = new Vector3(Random.Range(minX, maxX), 10f, 0.0f);
            }
            
            waitTimer += Time.deltaTime;
        }

        if (IsObjectsPaused && waitTimer >= waitTimerInterval)
        {
            IsObjectsPaused = false;
            waveTimer = 0;
            waitTimer = 0;

            speed = Random.Range(1f + waveCount, 4f + waveCount);
            
        }

    }
}
