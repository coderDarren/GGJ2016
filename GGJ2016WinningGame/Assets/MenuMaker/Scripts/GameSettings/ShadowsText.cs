using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShadowsText : MonoBehaviour {

    Text t;

    void Start()
    {
        t = GetComponent<Text>();
        SetShadowText();
    }

    void SetShadowText()
    {
        float shadowDist = QualitySettings.shadowDistance;
        if (shadowDist == 0)
            t.text = "Disabled";
        else
            t.text = "Enabled";
    }

    void Update()
    {
        SetShadowText();
    }
}
