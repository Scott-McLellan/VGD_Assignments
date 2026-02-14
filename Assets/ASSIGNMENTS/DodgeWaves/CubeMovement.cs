using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed;
    
    bool isGameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject sphere in spheres)
        {
            if (Vector3.Distance(transform.position, sphere.transform.position) <= 1)
            {
                Time.timeScale = 0;
                isGameOver = true;
            }
        }
        
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        
        transform.position += moveInput * speed * Time.deltaTime;

        if (transform.position.x >= 8f)
        {
            transform.position = new Vector3(8f, 0f, 0f);
        }

        if (transform.position.x <= -8f)
        {
            transform.position = new Vector3(-8f, 0f, 0f);
        }
        
        
        
    }
    
    
}
