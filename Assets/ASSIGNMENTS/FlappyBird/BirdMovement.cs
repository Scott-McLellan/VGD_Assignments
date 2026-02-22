using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    private AudioSource audioSource;
    public AudioClip audioClip;

    private bool isDead = false;
    
    public float forceAmmount;
    
    public void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;

        rb.freezeRotation = false;
        rb.angularVelocity = -250f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * forceAmmount, ForceMode2D.Impulse);
            PlaySound();
        }
        
    }
}
