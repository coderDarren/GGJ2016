using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextColorOvertime : MonoBehaviour {

    public Color[] textColors;
    public float colorSmooth = 5;
    public bool inOrder = false;

    Color currentColor;
    Color targetColor;
    Text txt;
    int colorIndex = 0;

    void Start()
    {
        targetColor = textColors[0];
        txt = GetComponent<Text>();
    }

    void Update()
    {
        if (ColorChanged())
        {
            if (inOrder)
            {
                if (colorIndex < textColors.Length - 1)
                    colorIndex++;
                else
                    colorIndex = 0;
            }
            else
            {
                colorIndex = Random.Range(0, textColors.Length - 1);
            }
            targetColor = textColors[colorIndex];
        }
        else
        {
            currentColor = Color.Lerp(currentColor, targetColor, colorSmooth * Time.deltaTime);
            txt.color = currentColor;
        }
    }

    bool ColorChanged()
    {
        if (Mathf.Abs(currentColor.r - targetColor.r) < 0.1f &&
            Mathf.Abs(currentColor.g - targetColor.g) < 0.1f &&
            Mathf.Abs(currentColor.b - targetColor.b) < 0.1f &&
            Mathf.Abs(currentColor.a - targetColor.a) < 0.1f)
        {
            return true;
        }
        return false;
    }

}
