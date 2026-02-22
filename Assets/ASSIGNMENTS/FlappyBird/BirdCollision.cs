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
        if (collision.gameObject.tag == "Pipe")
        {
            PlayPipeCrashSound();
            birdMovement.Die();
        }

        if (collision.gameObject.tag == "Ground")
        {
            PlayGroundSound();
            gameManager.GameOver();
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
