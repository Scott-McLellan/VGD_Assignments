using UnityEngine;


public class BirdCollision : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private AudioSource audioSource;
    private AudioSource audioSource2;
    public AudioClip audioClipPipe;
    public AudioClip audioClipCrash;
    
    private BirdMovement birdMovement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pipe"))
        {
            if (birdMovement.isDead)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
                return;
            }

            PlayPipeCrashSound();
            birdMovement.Die();

            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
            return;
        }

        if (collision.collider.CompareTag("Ground"))
        {
            PlayGroundSound();
            gameManager.GameOver();
            return;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource2 = GetComponent<AudioSource>();
        birdMovement = GetComponent<BirdMovement>();
        
    }

    public void PlayPipeCrashSound()
    {
        audioSource.PlayOneShot(audioClipPipe);
    }
    
    public void PlayGroundSound()
    {
        audioSource2.PlayOneShot(audioClipCrash);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
