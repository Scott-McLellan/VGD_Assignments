using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public GameObject birdPrefab;

    public int score;
    void OnTriggerEnter2D(Collider2D other)
    {
        score++;
        Debug.Log("Score: " + score);
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
