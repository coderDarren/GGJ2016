using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextureQualityText : MonoBehaviour {

    Text t;

    void Start()
    {
        t = GetComponent<Text>();
        SetTextureQualityText();
    }

    void SetTextureQualityText()
    {
        int quality = QualitySettings.masterTextureLimit;
        switch(quality)
        {
            case 0: t.text = "Full Resolution"; break;
            case 1: t.text = "Half Resolution"; break;
            case 2: t.text = "Quarter Resolution"; break;
            case 3: t.text = "Eighth Resolution"; break;
        }
    }

    void Update()
    {
        SetTextureQualityText();
    }
}
