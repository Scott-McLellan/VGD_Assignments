using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody player;

    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 10f;
    
    public float groundCheckDistance = 1.1f;
    public LayerMask groundLayer;

    float xRotation = 0f;
    
    bool canJump = true;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Jump()
    {
        player.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        
        
        if (Physics.Raycast(ray, out RaycastHit hit,  groundCheckDistance, groundLayer))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
        
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        transform.position += move * currentSpeed * Time.deltaTime;
        

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            canJump = false;
        }
        Debug.DrawRay(transform.position, -transform.up, Color.red);
    }
}
