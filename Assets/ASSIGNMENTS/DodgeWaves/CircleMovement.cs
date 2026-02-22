using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Vector3 centerPosition;
    public float speed;
    public float radius;
    private float angle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime * speed;
        
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);
        
        transform.position = new Vector3(centerPosition.x + x, centerPosition.y + y, centerPosition.z);
        
    }
}
