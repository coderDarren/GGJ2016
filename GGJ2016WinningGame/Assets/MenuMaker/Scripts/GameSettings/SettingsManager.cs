using UnityEngine;
using System.Collections;

public class SettingsManager : MonoBehaviour {

	public void SetQualityLevel(string level)
    {
        switch(level)
        {
            case "fastest": QualitySettings.SetQualityLevel(0, true); break;
            case "fast": QualitySettings.SetQualityLevel(1, true); break;
            case "simple": QualitySettings.SetQualityLevel(2, true); break;
            case "good": QualitySettings.SetQualityLevel(3, true); break;
            case "beautiful": QualitySettings.SetQualityLevel(4, true); break;
            case "fantastic": QualitySettings.SetQualityLevel(5, true); break;
        }
        QualitySettings.vSyncCount = 1;
    }

    public void SetTextureQuality(string quality)
    {
        switch(quality)
        {
            case "full": QualitySettings.masterTextureLimit = 0; break;
            case "half": QualitySettings.masterTextureLimit = 1; break;
            case "quarter": QualitySettings.masterTextureLimit = 2; break;
            case "eighth": QualitySettings.masterTextureLimit = 3; break;
        }
    }

    public void SetAntiAliasing(string value)
    {
        switch(value)
        {
            case "disable": QualitySettings.antiAliasing = 0; break;
            case "2x": QualitySettings.antiAliasing = 2; break;
            case "4x": QualitySettings.antiAliasing = 4; break;
            case "8x": QualitySettings.antiAliasing = 8; break;
        }
    }

    public void SetShadows(string shadows)
    {
        switch(shadows)
        {
            case "disable": QualitySettings.shadowDistance = 0; break;
            case "enable": QualitySettings.shadowDistance = 150; break;
        }
    }
}
