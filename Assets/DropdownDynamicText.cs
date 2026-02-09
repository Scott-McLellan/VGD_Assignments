using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    public TMP_Dropdown dropdown;
    public TMP_Text valueText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateText(dropdown.value);
        dropdown.onValueChanged.AddListener(UpdateText);
    }

    void UpdateText(int index)
    {
        valueText.text = dropdown.options[index].text;
    }
}
