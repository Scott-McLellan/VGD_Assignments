using UnityEngine;
using TMPro;
public class Play : MonoBehaviour
{
    public GameObject playButton;
    public GameObject pauseButton;
    [SerializeField] public TMP_Text scoreText;

    public void StartGame()
    {
        Begin();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        scoreText.text = "";
        
    }

    public void Begin()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        playButton.SetActive(false);
        scoreText.text = "0";
        
    }
}
