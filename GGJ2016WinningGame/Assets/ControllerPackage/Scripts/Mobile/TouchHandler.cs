using UnityEngine;
using System.Collections;

public class TouchHandler : MonoBehaviour {

	Touch touch;

	public Touch TouchInput { get { return touch; } }

	public TouchHandler()
	{

	}

	public void UpdateTouchInput()
	{
		if (Input.touchCount == 1){
			touch = Input.GetTouch(0);
		}
	}

	public Vector2 DragInput()
	{
		return touch.deltaPosition;
	}
}
