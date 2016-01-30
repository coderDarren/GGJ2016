using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSCameraSettings : MonoBehaviour {

	FPSCamera camera;

    public Slider yOffsetField;
    public Toggle useSmoothLookField;
    public Slider smoothLookTimeField;
    public Slider xSensitivityField, ySensitivityField;
    public Toggle useBounceField;
    public Slider bounceFrequencyField, bounceAmplitudeField;

    public void InitializeSettings()
    {
        camera = Camera.main.GetComponent<FPSCamera>();

        yOffsetField.value = PlayerPrefs.GetFloat(CameraSettings.Instance.FPS_LOCAL_Y_OFFSET);
        int useSmoothLook = PlayerPrefs.GetInt(CameraSettings.Instance.FPS_USE_SMOOTH_LOOK);
        if (useSmoothLook == 0)
        	useSmoothLookField.isOn = false;
        else {
        	useSmoothLookField.isOn = true;
        }
        smoothLookTimeField.value = PlayerPrefs.GetFloat(CameraSettings.Instance.FPS_SMOOTH_LOOK_TIME);
        xSensitivityField.value = PlayerPrefs.GetFloat(CameraSettings.Instance.FPS_X_LOOK_SENSITIVITY);
        ySensitivityField.value = PlayerPrefs.GetFloat(CameraSettings.Instance.FPS_Y_LOOK_SENSITIVITY);
		int useBounce = PlayerPrefs.GetInt(CameraSettings.Instance.FPS_USE_BOUNCE);
        if (useBounce == 0)
        	useBounceField.isOn = false;
        else {
        	useBounceField.isOn = true;
        }
        bounceFrequencyField.value = PlayerPrefs.GetFloat(CameraSettings.Instance.FPS_BOUNCE_FREQUENCY);
        bounceAmplitudeField.value = PlayerPrefs.GetFloat(CameraSettings.Instance.FPS_BOUNCE_AMPLITUDE);
    }

    public void SetYOffset()
    {
    	camera.targetYOffset = yOffsetField.value;
    	PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_LOCAL_Y_OFFSET, yOffsetField.value);
    }

    public void SetUseSmoothLook()
    {
    	camera.smoothLook = useSmoothLookField.isOn;
    	if (camera.smoothLook)
    	{
    		PlayerPrefs.SetInt(CameraSettings.Instance.FPS_USE_SMOOTH_LOOK, 1);
    	}else {
    		PlayerPrefs.SetInt(CameraSettings.Instance.FPS_USE_SMOOTH_LOOK, 0);
    	}
    }

    public void SetSmoothLookTime()
    {
    	camera.smoothLookTime = smoothLookTimeField.value;
    	PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_SMOOTH_LOOK_TIME, smoothLookTimeField.value);
    }

    public void SetXSensitivity()
    {
    	camera.XSensitivity = xSensitivityField.value;
    	PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_X_LOOK_SENSITIVITY, xSensitivityField.value);
    }

    public void SetYSensitivity()
    {
    	camera.YSensitivity = ySensitivityField.value;
    	PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_Y_LOOK_SENSITIVITY, ySensitivityField.value);
    }

    public void SetUseBounce()
    {
    	camera.useBounce = useBounceField.isOn;
    	if (camera.useBounce)
    	{
    		PlayerPrefs.SetInt(CameraSettings.Instance.FPS_USE_BOUNCE, 1);
    	}else {
    		PlayerPrefs.SetInt(CameraSettings.Instance.FPS_USE_BOUNCE, 0);
    	}
    }

    public void SetBounceFrequency()
    {
    	camera.bounceFrequency = bounceFrequencyField.value;
    	PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_BOUNCE_FREQUENCY, bounceFrequencyField.value);
    }

    public void SetBounceAmplitude()
    {
    	camera.bounceAmplitude = bounceAmplitudeField.value;
    	PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_BOUNCE_AMPLITUDE, bounceAmplitudeField.value);
    }

    public void Reset()
    {
	    PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_LOCAL_Y_OFFSET, 2);
	    PlayerPrefs.SetInt(CameraSettings.Instance.FPS_USE_SMOOTH_LOOK, 1);
	    PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_SMOOTH_LOOK_TIME, 10);
	    PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_X_LOOK_SENSITIVITY, 200);
	    PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_Y_LOOK_SENSITIVITY, 320);
	    PlayerPrefs.SetInt(CameraSettings.Instance.FPS_USE_BOUNCE, 1);
	    PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_BOUNCE_FREQUENCY, 4);
	    PlayerPrefs.SetFloat(CameraSettings.Instance.FPS_BOUNCE_AMPLITUDE, 2);
        InitializeSettings();
    }
}
