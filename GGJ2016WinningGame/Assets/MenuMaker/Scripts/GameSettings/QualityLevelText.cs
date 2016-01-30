using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QualityLevelText : MonoBehaviour {


    Text t;

    void Start()
    {
        t = GetComponent<Text>();
        t.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }

    void Update()
    {
        t.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }
}
