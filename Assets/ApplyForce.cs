using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public float forceAmmount = 500f;
    
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb =  GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.forward * forceAmmount);
        }
    }
}
