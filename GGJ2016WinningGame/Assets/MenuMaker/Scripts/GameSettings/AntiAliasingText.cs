using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AntiAliasingText : MonoBehaviour {

    Text t;

    void Start()
    {
        t = GetComponent<Text>();
        SetAntiAliasingText();
    }

    void SetAntiAliasingText()
    {
        int aliasing = QualitySettings.antiAliasing;
        switch (aliasing)
        {
            case 0: t.text = "Disabled"; break;
            case 2: t.text = "2x Multi Sampling"; break;
            case 4: t.text = "4x Multi Sampling"; break;
            case 8: t.text = "8x Multi Sampling"; break;
        }
    }

    void Update()
    {
        SetAntiAliasingText();
    }
}
