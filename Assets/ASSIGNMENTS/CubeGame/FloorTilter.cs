using UnityEngine;

public class FloorTilter : MonoBehaviour
{
    public float tiltSpeed = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 tiltInput = new Vector3(vertical , 0f,-horizontal);
        
        transform.Rotate(tiltInput *  tiltSpeed * Time.deltaTime);
        
        
    }
}
