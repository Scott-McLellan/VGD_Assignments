using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScoreTrigger : MonoBehaviour
{
    public GameObject birdPrefab;
    
    [SerializeField] private TMP_Text scoreText;
    
    [SerializeField] private GameManager gameManager;
    
    public int score;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        score++;
        scoreText.text = score.ToString();
        gameManager.score = score;
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
