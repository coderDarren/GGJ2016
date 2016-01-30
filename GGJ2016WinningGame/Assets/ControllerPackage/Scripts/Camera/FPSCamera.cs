using UnityEngine;
using System.Collections;

///<summary>
///**********************************************************IMPORTANT MESSAGE*************************************************
///This script cares about this package's character controller.
///We will be turning the character from this class. So we remove turn control from the character controller.
///The PlayerController class has an fps mode bool that dictates whether it should be turning or strafing.
///We will be checking the controller's speed variable and whether the controller is grounded or not.
///Keep this in mind when integrating the camera with your own controllers.
///Your controller should have a speed/velocity float and a grounded boolean value.
///</summary>
public class FPSCamera : MonoBehaviour {

	//This package uses a PlayerController. You will need to modify this based on your own controller.
	public PlayerController controller; 
	public float targetYOffset = 2f;
	public bool smoothLook = true;
	public float smoothLookTime = 20f;
	public float XSensitivity = 200f;
	public float YSensitivity = 320f;
	public bool useBounce = true;
	public float bounceFrequency = 2f;
	public float bounceAmplitude = 0.7f;

	Quaternion targetRotation;
	Vector3 rotation = Vector3.zero;
	Vector2 look = Vector2.zero;
	Vector2 bouncePosition = Vector2.zero;
	Vector3 targetPos = Vector3.zero;

	public bool targetVisible = true;

	void Start()
	{
		SetCameraTarget(controller);
		transform.position = controller.transform.position + Vector3.up*targetYOffset;
	}

	void GetInput()
	{
		look.x = Input.GetAxis("Mouse X");
		look.y = Input.GetAxis("Mouse Y");
	}

	public void SetCameraTarget(PlayerController c)
    {
        controller = c;

        if (controller == null)
        {
            Debug.LogError("Your FPS Camera is looking for a PlayerController. Is your character using something else?");
        }
    }

    void Update()
    {
    	if (Input.GetKeyUp(KeyCode.Tab))
    		Cursor.visible = !Cursor.visible;

    	GetInput();
    	if (useBounce)
    		Bounce();
    	else {
    		bouncePosition = Vector2.zero;
    	}
    }

	void FixedUpdate()
	{
		targetPos = controller.transform.position + 
							 Vector3.up*targetYOffset +
							 Vector3.up*bouncePosition.y +
							 transform.TransformDirection(Vector3.right * bouncePosition.x);

		transform.position = Vector3.Lerp(transform.position, targetPos, 20*Time.deltaTime);
		
		TurnTarget();
		LookUpDown();
	}

	void LookUpDown()
	{
		rotation.x -= look.y * YSensitivity * Time.deltaTime;
		rotation.x = Mathf.Clamp(rotation.x, -60, 80);
		if (smoothLook){
			targetRotation = Quaternion.Slerp(targetRotation, controller.transform.rotation * Quaternion.Euler(rotation), smoothLookTime * Time.deltaTime);
			transform.rotation = targetRotation;
		}
		else {
			transform.rotation = controller.transform.rotation * Quaternion.Euler(rotation);
		}
	}

	void TurnTarget()
    {
        controller.transform.rotation *= Quaternion.AngleAxis(look.x * XSensitivity * Time.deltaTime, Vector3.up);
    }

    float targetX = 0, targetY = 0;
    void Bounce()
    {
    	//Controller needs a grounded bool - or a method called 'Grounded' that returns a bool.
		//Controller needs some float for forward movement and a float for strafe movement
		if (controller.Grounded() && (Mathf.Abs(controller.forwardInput) > 0.1f || Mathf.Abs(controller.turnInput) > 0.1f)){
			targetX = Mathf.PingPong(bounceFrequency*Time.time, bounceAmplitude);
    		targetY = Mathf.PingPong(bounceFrequency*Time.time, bounceAmplitude/2f);
    		bouncePosition.x = Mathf.Lerp(bouncePosition.x, targetX, bounceFrequency*Time.deltaTime);
    		bouncePosition.y = Mathf.Lerp(bouncePosition.y, targetY, bounceFrequency*Time.deltaTime);
		}
    }

    public void ToggleShowTarget()
    {
    	targetVisible = !targetVisible;
    	SkinnedMeshRenderer[] skinnedRenderers = controller.GetComponentsInChildren<SkinnedMeshRenderer>();
    	MeshRenderer[] renderers = controller.GetComponentsInChildren<MeshRenderer>();
    	foreach(SkinnedMeshRenderer skin in skinnedRenderers)
    		skin.enabled = targetVisible;
    	foreach(MeshRenderer mesh in renderers)
    		mesh.enabled = targetVisible;
    }
}
