using UnityEngine;
using System.Collections;

public class MultitouchHandler : MonoBehaviour {

	Vector3 touchOnePrev, touchTwoPrev;
	Touch touchOne, touchTwo;
	float prevDeltaMagnitude, currentDeltaMagnitude;
	float currentAngle, prevAngle, deltaAngle;

	float pinchInput = 0;
	float spinInput = 0;
	bool spinning = false;
	float minPinch, minAngle;

	public float PinchInput { get { return pinchInput; } }
	public float SpinInput { get { return spinInput; } }

	public MultitouchHandler(float minimumPinchWeight, float minimumTurnAngle)
	{
		minPinch = minimumPinchWeight;
		minAngle = minimumTurnAngle;
	}

	public void UpdateMultiTouchInput()
	{
		RegisterSpins();
		RegisterPinches();
	}

	void RegisterPinches()
	{
		if (Input.touchCount == 2 && !spinning)
		{
			touchOne = Input.GetTouch(0);
			touchTwo = Input.GetTouch(1);
			touchOnePrev = touchOne.position - touchOne.deltaPosition;
			touchTwoPrev = touchTwo.position - touchTwo.deltaPosition;
			prevDeltaMagnitude = (touchOnePrev - touchTwoPrev).magnitude;
			currentDeltaMagnitude = (touchOne.position - touchTwo.position).magnitude;
			if (Mathf.Abs(prevDeltaMagnitude - currentDeltaMagnitude) > minPinch){
				pinchInput = prevDeltaMagnitude - currentDeltaMagnitude;
			}
			else {
				pinchInput = 0;
			}
		} else {
			pinchInput = 0;
		}
	}

	float smoothVel = 0;
	void RegisterSpins()
	{
		if (Input.touchCount == 2)
		{
			touchOne = Input.GetTouch(0);
			touchTwo = Input.GetTouch(1);
			touchOnePrev = touchOne.position - touchOne.deltaPosition;
			touchTwoPrev = touchTwo.position - touchTwo.deltaPosition;
			currentAngle = Angle(touchOne.position, touchTwo.position);
			prevAngle = Angle(touchOnePrev, touchTwoPrev);
			deltaAngle = Mathf.DeltaAngle(currentAngle, prevAngle);
			if (Mathf.Abs(deltaAngle) > minAngle){
				spinInput = deltaAngle * (Mathf.PI / 2);
				spinning = true;
			}
			else {
				spinInput = 0;
				deltaAngle = 0;
				spinning = false;
			}
		}
		else {
			spinInput = 0;
		}
	}

	float angle = 0;
	Vector2 from, to;
	Vector3 cross;
	float Angle(Vector2 v1, Vector2 v2)
	{
		from = v1 - v2;
		to = Vector2.right;
		cross = Vector3.Cross(from, to);
		angle = Vector2.Angle(from, to);
		if (cross.z > 0)
		{
			angle = 360f - angle;
		}
		return angle;
	}
}
