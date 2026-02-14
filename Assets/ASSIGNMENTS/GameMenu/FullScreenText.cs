using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FullScreenText : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Text text;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateText(toggle.isOn);
        toggle.onValueChanged.AddListener(UpdateText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateText(bool isFullScreen)
    {
        text.text = isFullScreen ? "Fullscreen" : "Windowed";
    }
}
