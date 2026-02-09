using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float score;

    public float time;
    
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
        
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }
    
    
}
