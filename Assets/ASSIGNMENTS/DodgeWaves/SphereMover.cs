using UnityEngine;

public class SphereMover : MonoBehaviour
{
    public float speed;
    
    private float waitTimer = 0f;
    private float lookDirection = 1f;

    
    
    
    

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
        if (cubes.Length == 0)
        {
           return; 
        }

        if (spheres.Length == 0)
        {
            return;
        }

        Transform closest = cubes[0].transform;
        Transform sphere = spheres[0].transform;
        
        transform.position += new Vector3(lookDirection, 0f, 0f) * speed * Time.deltaTime;
        
        float best = Vector3.Distance(transform.position, closest.position);
        float bestSphere = Vector3.Distance(transform.position, sphere.position);
        
        for (int i = 0; i < spheres.Length; i++)
        {
            if (spheres[i] == gameObject) continue;
            
            float distSphere = Vector3.Distance(transform.position, spheres[i].transform.position);
            if (distSphere < 1)
            {
                bestSphere = distSphere;
                lookDirection *= -1f;
            }
        }

        for (int i = 0; i < cubes.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, cubes[i].transform.position);
            if (dist < 1)
            {
                best = dist;
                lookDirection *= -1f;
                Debug.Log("GAME OVER");
            }
        }
        Debug.Log(Time.deltaTime);
    }
}
