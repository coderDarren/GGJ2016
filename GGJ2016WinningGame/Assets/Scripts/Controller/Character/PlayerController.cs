using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVel = 12;
        public float rotateVel = 3;
        public float mouseRotateVel = 0.1f;
        public float jumpVel = 5;
        public float distToGrounded = 0.1f;
        public Transform capsuleTop, capsuleBottom;
        public float capsuleRadius;
        public LayerMask ground;
    }

    [System.Serializable]
    public class PhysSettings
    {
        public float downAccel = 0.3f;
    }

    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
    }

    public MoveSettings moveSetting = new MoveSettings();
    public PhysSettings physSetting = new PhysSettings();
    public InputSettings inputSetting = new InputSettings();

    Vector3 velocity = Vector3.zero;
    Vector3 terrainNormal = Vector3.zero;
    Quaternion targetRotation;
    Rigidbody rBody;
    public float forwardInput, turnInput, jumpInput;
    public bool thirdPersonShooter = false; //Set to true when using FPSCamera
    public bool crouch = false, walk = false;
    public bool grounded;
    StandardCamera standardCam;
    FPSCamera fpsCam;
    float modifiedSpeed = 1;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    public bool Grounded()
    { 
        if (Physics.CheckCapsule(moveSetting.capsuleTop.position, moveSetting.capsuleBottom.position, moveSetting.capsuleRadius, moveSetting.ground))
            return true;
        return false;
    }

    void Start()
    {
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("The character needs a rigidbody.");

        forwardInput = turnInput = jumpInput = 0;
        standardCam = Camera.main.GetComponent<StandardCamera>();
        fpsCam = Camera.main.GetComponent<FPSCamera>();
        fpsCam.enabled = false;
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis(inputSetting.FORWARD_AXIS); //interpolated
        if (forwardInput < -0.5f)
            forwardInput = -0.5f;
        turnInput = Input.GetAxis(inputSetting.TURN_AXIS); //interpolated
        jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS); //non-interpolated
        if (Input.GetKeyDown(KeyCode.LeftControl) && !walk)
        {
            crouch = !crouch;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            thirdPersonShooter = !thirdPersonShooter;
            SwitchCameras();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !crouch)
        {
            walk = !walk;
        }
    }

    void Update()
    {
        GetInput();
        if (crouch)
            modifiedSpeed = Mathf.Lerp(modifiedSpeed, 0.4f, 4 * Time.deltaTime);
        if (walk)
            modifiedSpeed = Mathf.Lerp(modifiedSpeed, 0.3f, 4 * Time.deltaTime);
        if (!crouch && !walk)
            modifiedSpeed = Mathf.Lerp(modifiedSpeed, 1, 4 * Time.deltaTime);
    }

    void FixedUpdate()
    {
        grounded = Grounded();

        Run();

        if (!thirdPersonShooter)
        {
            Turn();
            velocity.x = 0; //cant be strafing
        }
        else
        {
            Strafe();
        }
        
        Jump();

        rBody.velocity = transform.TransformDirection(velocity);
    }


    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputSetting.inputDelay)
        {
            //move
            velocity.z = moveSetting.forwardVel * forwardInput * modifiedSpeed;
        }
        else
            //zero velocity
            velocity.z = 0;
    }

    void Strafe()
    {
        if (Mathf.Abs(turnInput) > inputSetting.inputDelay)
        {
            //move
            velocity.x = moveSetting.forwardVel * turnInput * modifiedSpeed;
        }
        else
            //zero velocity
            velocity.x = 0;
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputSetting.inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {
        if (jumpInput > 0 && grounded)
        {
            //jump
            if (forwardInput > 0 && !crouch && !walk)
                velocity.y = moveSetting.jumpVel;
        }
        else if (jumpInput == 0 && grounded)
        {
            //zero out our velocity.y
            velocity.y = 0;
        }
        else
        {
            //decrease velocity.y
            velocity.y -= physSetting.downAccel;
        }
    }

    void OnEnable()
    {
        try {
            CameraSwitch.TogglePlayerFPSMode += ToggleFPSMode;
        }catch(System.Exception) {}
    }

    void OnDisable()
    {
        try {
            CameraSwitch.TogglePlayerFPSMode -= ToggleFPSMode;
        }catch(System.Exception) {}
    }

    void SwitchCameras()
    {
        if (thirdPersonShooter)
        {
            fpsCam.enabled = true;
            standardCam.enabled = false;
            fpsCam.rotation.x = standardCam.orbit.xRotation*-1;
            fpsCam.rotation.y = standardCam.orbit.yRotation+180;
            Cursor.visible = false;
        }
        else
        {
            fpsCam.enabled = false;
            standardCam.enabled = true;
            standardCam.orbit.xRotation = fpsCam.rotation.x*-1;
            standardCam.orbit.yRotation = fpsCam.rotation.y-180;
            Cursor.visible = true;
        }
    }

    void ToggleFPSMode(bool active)
    {
        thirdPersonShooter = active;
    }
}
