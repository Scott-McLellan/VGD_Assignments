using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScoreTrigger : MonoBehaviour
{
    public GameObject birdPrefab;
    
    private GameManager gameManager;
    
    private AudioSource audioSource;
    public AudioClip audioClip;
    
    private BirdMovement birdMovement;
    public int score;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        birdPrefab = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        birdMovement = birdPrefab.GetComponent<BirdMovement>();
    }
    
    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (birdMovement.isDead) return;
            
            gameManager.AddScore(1);
            PlaySound();
        } 
    }
}
