﻿using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour {

    public LayerMask groundLayer;

    [System.Serializable]
    public class PositionSettings
    {
        public bool invertPan = true;
        public float panSmooth = 7f;
        public float distanceFromGround = 40;
        public bool allowZoom = true;
        public float zoomSmooth = 5;
        public float zoomStep = 5;
        public float maxZoom = 25;
        public float minZoom = 80;

        [HideInInspector]
        public float newDistance = 40;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = 50;
        public float yRotation = 0;
        public bool allowYOrbit = true;
        public float yOrbitSmooth = 0.5f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string PAN = "Fire1";
        public string ORBIT_Y = "Fire2";
        public string ZOOM = "Mouse ScrollWheel";
    }

    [System.Serializable]
    public class MobileSettings
    {
        public bool useMobileInput = false;
        public float minimumPinchDelta = 0.1f;
        public float minimumTurnAngle = 0.1f;
    }
    TouchHandler singleTouch;
    MultitouchHandler multiTouch;

    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();
    public MobileSettings mobile = new MobileSettings();

    Vector3 destination = Vector3.zero;
    Vector3 camVel = Vector3.zero;
    float panInput, orbitInput, zoomInput;
    int panDirection = 0;

    bool editingSettings = false;

    void Start()
    {
        //initialization code
        panInput = 0;
        orbitInput = 0;
        zoomInput = 0;
        singleTouch = new TouchHandler();
        multiTouch = new MultitouchHandler(mobile.minimumPinchDelta, mobile.minimumTurnAngle);
    }

    void GetInput()
    {
        //responsible for setting our input variables
        if (!editingSettings)
        {
            if (!mobile.useMobileInput)
            {
                try{ panInput = Input.GetAxis(input.PAN); }
                catch (System.Exception) { Debug.Log("Invalid pan input. Please insert a valid value such as Fire1"); }
                try { orbitInput = Input.GetAxis(input.ORBIT_Y); }
                catch (System.Exception) { Debug.Log("Invalid orbit input. Please insert a valid value such as Fire2"); }
                zoomInput = Input.GetAxis(input.ZOOM);
            }
            else {
                singleTouch.UpdateTouchInput();
                multiTouch.UpdateMultiTouchInput();
                zoomInput = -multiTouch.PinchInput;
                orbitInput = multiTouch.SpinInput;
            }
        }
    }

    void Update()
    {
        //updating input
        GetInput();
        if (!editingSettings)
        {
            if (!mobile.useMobileInput)
            {
                if (position.allowZoom)
                    Zoom();
                //rotating
                if (orbit.allowYOrbit)
                    Rotate();
                //panning
                PanWorld();
            }else {
                if (position.allowZoom)
                    Zoom();
                if (orbit.allowYOrbit)
                    RotateMobile();
                PanWorldMobile();
            }
        }
        transform.rotation = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0);
    }

    void FixedUpdate()
    {
        HandleCameraDistance();
    }

    Vector3 targetPos = Vector3.zero;
    void PanWorld()
    {
        targetPos = transform.position;

        if (position.invertPan)
            panDirection = -1;
        else
            panDirection = 1;

        if (panInput > 0)
        {
            targetPos += transform.right * Input.GetAxis("Mouse X") * position.panSmooth * panDirection * Time.deltaTime;
            targetPos += Vector3.Cross(transform.right, Vector3.up) * Input.GetAxis("Mouse Y") * position.panSmooth * panDirection * Time.deltaTime;
        }
        transform.position = targetPos;
    }

    void PanWorldMobile()
    {
        targetPos = transform.position;

        if (position.invertPan)
            panDirection = -1;
        else
            panDirection = 1;

        if (singleTouch.TouchInput.phase == TouchPhase.Moved &&
            Input.touchCount == 1)
        {
            targetPos += transform.right * singleTouch.DragInput().x * position.panSmooth * panDirection * Time.deltaTime;
            targetPos += Vector3.Cross(transform.right, Vector3.up) * singleTouch.DragInput().y * position.panSmooth * panDirection * Time.deltaTime;
        }
        transform.position = targetPos;
    }

    void HandleCameraDistance()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, groundLayer))
        {
            destination = Vector3.Normalize(transform.position - hit.point) * position.distanceFromGround;
            destination += hit.point;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, 0.3f);
        }
    }

    void Zoom()
    {
        position.newDistance += -zoomInput;
        position.newDistance = Mathf.Clamp(position.newDistance, position.maxZoom, position.minZoom);
        position.distanceFromGround = Mathf.Lerp(position.distanceFromGround, position.newDistance, position.zoomSmooth * Time.deltaTime);
    }

    void Rotate()
    {
        if (orbitInput > 0)
        {
            orbit.yRotation += Input.GetAxis("Mouse X") * orbit.yOrbitSmooth * Time.deltaTime;
        }
    }

    void RotateMobile()
    {
        orbit.yRotation += orbitInput * orbit.yOrbitSmooth * 6.5f * Time.deltaTime;
    }

    void OnEnable()
    {
        RTSCameraSettings.IsEditing += IsEditing;
    }

    void OnDisable()
    {
        RTSCameraSettings.IsEditing -= IsEditing;
    }

    void IsEditing(bool editing)
    {
        editingSettings = editing;
    }
}
