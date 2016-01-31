using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    [System.Serializable]
    public struct FillThreshold { public float min; public float max; }
    [System.Serializable]
    public struct ValueThreshold { public float min; public float max; }
    public ValueThreshold valueThreshold;
    public FillThreshold fillThreshold;
    public enum BarType
    {
        Health,
        Xp,
        Energy
    }
    public BarType barType;

    Image bar;
    float currFill;

    bool filled = false;

    void Start()
    {
        bar = GetComponent<Image>();
        currFill = fillThreshold.min;
    }

    void Update()
    {
        switch (barType)
        {
            case BarType.Energy:
                SetFill(ref bar, (int)PlayerData.Instance.maxEnergy, (int)PlayerData.Instance.currEnergy);
                break;
            case BarType.Health:
                SetFill(ref bar, (int)PlayerData.Instance.maxHealth, (int)PlayerData.Instance.currHealth);
                break;
            case BarType.Xp:
                SetFill(ref bar, PlayerData.Instance.maxXP, PlayerData.Instance.currXP);
                break;
        }

    }
   

    public static void SetFill(ref Image bar, int valueMax, int valueCurr)
    {
        if (bar)
            bar.fillAmount = ((valueCurr) / (float)valueMax);
    }
    
}
