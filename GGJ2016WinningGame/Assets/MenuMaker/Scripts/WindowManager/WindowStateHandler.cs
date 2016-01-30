using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[RequireComponent(typeof(WindowState))]
[System.Serializable]
public class WindowStateHandler : MonoBehaviour {
	
    public List<State> states = new List<State>();

    [System.Serializable]
    public class WindowEvent : UnityEvent<string, string, string, bool> { }

    [System.Serializable]
    public class State
    {
        public string stateName = "defaultState";
        public bool active = false;
        public List<Action> possibleActions;

        public bool nameSaved = false;
    }

    [System.Serializable]
    public class Action
    {
        public string actionName = "defaultAction";

        public bool nameSaved = false;
        
        //unity event fields
        public bool useUnityEvent = false;
        public UnityEvent StartAction;

        //window event fields
        public bool useWindowEvent = false;
        public WindowEvent windowAction;
        public string resourceFolder;
        public string newPageName;
        public string basePageName;
        public bool removeAllPages = false;

        //action update fields
		public bool continuouslyInvokeAction = false;
        public enum UpdateStyle { Update, FixedUpdate, LateUpdate };
        public UpdateStyle updateFunction;
    }

	public void RefreshStates()
    {
		states.Clear ();
    }

    public void AddState()
    {
        State newState = new State();
        newState.active = false;
        newState.possibleActions = new List<Action>();
    }

    /// <summary>
    /// Searches through the state list to find the state 'stateName'
    /// </summary>
    /// <param name="stateName"></param>
    /// <returns></returns>
    public State FindState(string stateName)
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].stateName == stateName)
                return states[i];
        }

        return null;
    }
}
