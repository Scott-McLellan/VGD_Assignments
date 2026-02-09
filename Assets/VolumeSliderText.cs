using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSliderText : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateText(float value)
    {
        int percent = (int)(value);
        text.text = percent + "%";
    }
}
