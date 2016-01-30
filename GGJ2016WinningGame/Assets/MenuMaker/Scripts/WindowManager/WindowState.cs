using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ALL STATE CHANGING EVENTS PASS THROUGH HERE
/// SOME EVENTS ARE INVOKED HERE, OTHERS MAY BE INVOKED FROM A GAME MANAGER
/// </summary>
public class WindowState : MonoBehaviour {

    public WindowStateHandler.State currentState;
    public WindowStateHandler.Action currentAction;
    public string state, action;
    public string initialState = "SET THE INITIAL STATE";
    WindowStateHandler windowHandler;


    void Start()
    {
        windowHandler = GetComponent<WindowStateHandler>();
		SetState(initialState);
	}
	

    void Update()
    {
        //see definition below
        InvokeCurrentStateAction(WindowStateHandler.Action.UpdateStyle.Update);
    }

    void FixedUpdate()
    {
        InvokeCurrentStateAction(WindowStateHandler.Action.UpdateStyle.FixedUpdate);
    }

    void LateUpdate()
    {
        InvokeCurrentStateAction(WindowStateHandler.Action.UpdateStyle.LateUpdate);
    }

    /// <summary>
    /// ONLY USE THIS TO SET STATES
    /// </summary>
    /// <param name="state"></param>
    public void SetState(string state)
    {
        WindowStateHandler.State stateNode = windowHandler.FindState(state);
            
		if (stateNode != null && stateNode.stateName != currentState.stateName) {
            Debug.Log("Setting state: " + state);

            WindowStateHandler.State switchTo = stateNode;
			currentState.active = false;
			currentState = switchTo;
			currentState.active = true;
            this.state = currentState.stateName;
           // Debug.Log("Setting state");
           
            //check each action
            //if we are not continuously invoking, invoke once here
            //otherwise it will be continuously invoked in update, fixedupdate, or lateupdate
            foreach (WindowStateHandler.Action action in currentState.possibleActions)
            {
                if (!action.continuouslyInvokeAction)
                {
                    action.StartAction.Invoke();
                    action.windowAction.Invoke(action.newPageName,
                                               action.basePageName,
                                               action.resourceFolder,
                                               action.removeAllPages);
                }

            }
        }
        else if (stateNode == null)
        {
            Debug.LogError("The state you are trying to switch to is not valid.");
        }

        
    }
    

    /// <summary>
    /// Used to manage invoking all actions within the current state.
    /// The update type is passed in to determine if the action should update inside Update(), FixedUpdate(), or LateUpdate()
    /// </summary>
    /// <param name="UpdateType"></param>
    void InvokeCurrentStateAction(WindowStateHandler.Action.UpdateStyle UpdateType)
    {
        foreach (WindowStateHandler.Action action in currentState.possibleActions)
        {
            //these conditions must be met in order to invoke the action in the update type
            if (action.updateFunction == UpdateType && action.continuouslyInvokeAction)
            {
                action.StartAction.Invoke();
            }
        }
    }


    
}
