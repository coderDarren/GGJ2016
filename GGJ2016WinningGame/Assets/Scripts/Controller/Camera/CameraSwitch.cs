using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

    public delegate void FPSToggleDelegate(bool active);
    public static event FPSToggleDelegate TogglePlayerFPSMode;

    StandardCamera standard;
    TopDownCamera topDown;
    RTSCamera rts;
    FPSCamera fps;

    public StandardCameraSettings standardSettings;
    public TopDownCameraSettings topdownSettings;
    public RTSCameraSettings rtsSettings;
    public FPSCameraSettings fpsSettings;

    public void Init()
    {
        Profiler.maxNumberOfSamplesPerFrame = -1;
        standard = Camera.main.GetComponent<StandardCamera>();
        topDown = Camera.main.GetComponent<TopDownCamera>();
        rts = Camera.main.GetComponent<RTSCamera>();
        fps = Camera.main.GetComponent<FPSCamera>();
        
        SwitchToCamera(PlayerPrefs.GetInt(CameraSettings.Instance.CAMERA_TYPE));
    }

    void SwitchToStandard()
    {
        standard.enabled = true;
        standardSettings.gameObject.SetActive(true);
        topDown.enabled = false;
        topdownSettings.gameObject.SetActive(false);
        rts.enabled = false;
        rtsSettings.gameObject.SetActive(false);
        fps.enabled = false;
        fpsSettings.gameObject.SetActive(false);
        standardSettings.InitializeSettings();

         if (!fps.targetVisible)
            fps.ToggleShowTarget();

        try{TogglePlayerFPSMode(false);}
        catch(System.Exception){}
    }

    void SwitchToTopDown()
    {
        standard.enabled = false;
        standardSettings.gameObject.SetActive(false);
        topDown.enabled = true;
        topdownSettings.gameObject.SetActive(true);
        rts.enabled = false;
        rtsSettings.gameObject.SetActive(false);
        fps.enabled = false;
        fpsSettings.gameObject.SetActive(false);
        topdownSettings.InitializeSettings();

         if (!fps.targetVisible)
            fps.ToggleShowTarget();

        try{TogglePlayerFPSMode(false);}
        catch(System.Exception){}
    }

    void SwitchToRTS()
    {
        standard.enabled = false;
        standardSettings.gameObject.SetActive(false);
        topDown.enabled = false;
        topdownSettings.gameObject.SetActive(false);
        rts.enabled = true;
        rtsSettings.gameObject.SetActive(true);
        fps.enabled = false;
        fpsSettings.gameObject.SetActive(false);
        rtsSettings.InitializeSettings();

        if (!fps.targetVisible)
            fps.ToggleShowTarget();

        try{TogglePlayerFPSMode(false);}
        catch(System.Exception){}

        try
        {
            standard.target.GetComponentInChildren<ObstructionHandler>().obstructionSetting.active = false;
            standard.target.GetComponentInChildren<ObstructionHandler>().obstructionSetting.changeTargetColor = false;
        }
        catch (System.Exception) { }
        try
        {
            topDown.target.GetComponentInChildren<ObstructionHandler>().obstructionSetting.active = false;
            topDown.target.GetComponentInChildren<ObstructionHandler>().obstructionSetting.changeTargetColor = false;
        }
        catch (System.Exception) { }
    }

    void SwitchToFPS()
    {
        standard.enabled = false;
        standardSettings.gameObject.SetActive(false);
        topDown.enabled = false;
        topdownSettings.gameObject.SetActive(false);
        rts.enabled = false;
        rtsSettings.gameObject.SetActive(false);
        fps.enabled = true;
        fpsSettings.gameObject.SetActive(true);
        fpsSettings.InitializeSettings();

        if (fps.targetVisible)
            fps.ToggleShowTarget();

        try{TogglePlayerFPSMode(true);}
        catch(System.Exception){}

        try
        {
            standard.target.GetComponentInChildren<ObstructionHandler>().obstructionSetting.active = false;
            standard.target.GetComponentInChildren<ObstructionHandler>().obstructionSetting.changeTargetColor = false;
        }
        catch (System.Exception) { }
        try
        {
            topDown.target.GetComponentInChildren<ObstructionHandler>().obstructionSetting.active = false;
            topDown.target.GetComponentInChildren<ObstructionHandler>().obstructionSetting.changeTargetColor = false;
        }
        catch (System.Exception) { }
    }

    public void SwitchToCamera(int cam_type)
    {
        switch(cam_type)
        {
            case 0: SwitchToStandard(); break;
            case 1: SwitchToTopDown(); break;
            case 2: SwitchToRTS(); break;
            case 3: SwitchToFPS(); break;
        }
    }
}
