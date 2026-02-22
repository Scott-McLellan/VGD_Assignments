using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    private AudioSource audioSource;
    public AudioClip audioClip;

    public bool isDead = false;
    
    public float forceAmmount;
    
    private Animator animator;
    
    public void Die()
    {
        if (isDead) return;
        
        isDead = true;
        rb.linearVelocity = Vector2.zero;

       
        rb.angularVelocity = -250f;
        rb.linearVelocity = Vector2.zero;
        
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    
    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * forceAmmount, ForceMode2D.Impulse);
            PlaySound();
            animator.SetTrigger("Flap");
        }
        
    }
}
