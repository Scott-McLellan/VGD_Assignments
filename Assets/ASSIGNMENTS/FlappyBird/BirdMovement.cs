using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public float forceAmmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * forceAmmount, ForceMode2D.Impulse);
        }
    }
}
