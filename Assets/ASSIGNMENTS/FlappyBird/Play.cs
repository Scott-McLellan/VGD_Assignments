using UnityEngine;
using TMPro;

public class Play : MonoBehaviour
{
    public GameObject playButton;
    public GameObject pauseButton;
    public TMP_Text scoreText;

    public AudioClip audioClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 0;
        pauseButton.SetActive(false);
        scoreText.text = "";
    }

    public void StartGame()
    {
        Debug.Log("StartGame clicked");

        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }

        Begin();
    }

    void Begin()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        playButton.SetActive(false);
        scoreText.text = "0";
    }
}