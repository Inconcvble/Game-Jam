using UnityEngine;
using TMPro;

public class GlitchEffect : MonoBehaviour
{
    public TMP_Text text;
    string originalText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalText = text.text;
        InvokeRepeating("Glitch", 2f, 3f); 
    }

    void Glitch()
    {
        text.text = "M!ND'S B0X";
        Invoke("ResetText", 0.2f);
    }

    void ResetText()
    {
        text.text = originalText;
    }
}
