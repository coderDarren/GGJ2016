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
        Energy,
        Speed,
        Stealth,
        Damage
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
                SetFill(ref bar, (int)PlayerData.Instance.maxXP, (int)PlayerData.Instance.currXP);
                break;
            case BarType.Speed:
                SetFill(ref bar, (int)PlayerData.Instance.maxSpeed, (int)PlayerData.Instance.currSpeed);
                break;
            case BarType.Stealth:
                SetFill(ref bar, (int)PlayerData.Instance.maxStealth, (int)PlayerData.Instance.currStealth);
                break;
            case BarType.Damage:
                SetFill(ref bar, (int)PlayerData.Instance.maxDamage, (int)PlayerData.Instance.currDamage);
                break;
        }

    }
   

    public static void SetFill(ref Image bar, int valueMax, int valueCurr)
    {
        if (bar)
            bar.fillAmount = Mathf.Lerp(bar.fillAmount, ((valueCurr) / (float)valueMax), 5*Time.deltaTime);
    }
    
}
