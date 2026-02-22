using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float sphereSpawnTimer;
    public float sphereInterval;
    
    public float zigZagSphereTimer;
    public float zigZagSphereInterval;
    
    private float score;

    public float time;
    
    public GameObject SpherePrefab;
    
    public GameObject ZigZagSpherePrefab;
    
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        score = time / 2;
        
        sphereSpawnTimer += Time.deltaTime;
        zigZagSphereTimer += Time.deltaTime;
        
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");

        foreach (GameObject sphere in spheres)
        {
            SpereVerticalMovement sphereMovement = sphere.GetComponent<SpereVerticalMovement>();
            
            
        }

        if (sphereSpawnTimer >= sphereInterval)
        {
            Instantiate(SpherePrefab, new Vector3(Random.Range(-7, 7), 7.5f, 0.0f), Quaternion.identity);
            
            sphereSpawnTimer -= sphereInterval;
        }

        if (zigZagSphereTimer >= zigZagSphereInterval)
        {
            Instantiate(ZigZagSpherePrefab, new Vector3(Random.Range(-7, 7), 7.5f, 0.0f), Quaternion.identity);
            
            zigZagSphereTimer -= zigZagSphereInterval;
        }

        if (Time.timeScale == 0)
        {
            Debug.Log($"Total Time Survived: {time}");
            Debug.Log("GAME OVER! Score: " + score);
        }
    }
}
