using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float score;

    public float time;
    
    public float highScore;
    
    public TextMeshProUGUI highScoreText;
    
    
    [SerializeField] private TextMeshProUGUI scoreText; //For the UI
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: " + Mathf.FloorToInt(score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        score = time / 2;

        score = highScore;

        if (score > highScore)
        {
            highScore = score;
        }
        
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
        
        highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore);
    }
    
    
}
