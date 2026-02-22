using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZigZagSphereMovement : MonoBehaviour
{
    public float speed;
    public float zigZagSpeed = 4f;
    public float zigZagWidth = 2f;

    public float minX;
    
    public float maxX;
    
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
        
        float sinMove =  Mathf.Sin(Time.time * zigZagSpeed) * zigZagWidth;
        
        if (!IsObjectsPaused)
        {
            transform.position += new Vector3(sinMove, -1.0f, 0.0f) * speed * Time.deltaTime;
        }

        if (!IsObjectsPaused && transform.position.y <= -5.5)
        {
            speed = Random.Range(1f, 4f);
            transform.position = new Vector3(Random.Range(minX, maxX), 8f, 0.0f * speed * Time.deltaTime);
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
