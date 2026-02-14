using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    public GameObject gameOverPanel;
    public GameObject pauseButton;

    public int score;
    public int highScore;
    public bool isGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        isGameOver = false;
        score = 0;

        gameOverPanel.SetActive(false);
        
        scoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        
        score += amount;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        scoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + highScore;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        gameOverPanel.SetActive(true);
        pauseButton.SetActive(false);
        
        Time.timeScale = 0;
    }
    
}
