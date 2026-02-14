using UnityEngine;

public class PipeMovement : MonoBehaviour
{

    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new  Vector3(-1, 0, 0) * speed * Time.deltaTime;

        if (transform.position.x <= -5)
        {
            Destroy(gameObject);
        }
    }
}
