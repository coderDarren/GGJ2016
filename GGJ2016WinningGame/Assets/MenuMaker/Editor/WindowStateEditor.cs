using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WindowState))]
[CanEditMultipleObjects]
public class WindowStateEditor : Editor {

    SerializedProperty currentState, currentAction, initialState;

	void OnEnable()
    {
        currentState = serializedObject.FindProperty("state");
        currentAction = serializedObject.FindProperty("action");
		initialState = serializedObject.FindProperty ("initialState");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

		EditorGUILayout.PropertyField (initialState);

		Space();

        GUILayout.BeginHorizontal();
        GUI.backgroundColor = new Color(75f / 255f, 118f / 255f, 151f / 255f);
        GUILayout.Box("Current State", GUILayout.MinWidth(Screen.width / 4));
        GUILayout.Space(5);
        GUI.backgroundColor = Color.white;
        GUILayout.Box(currentState.stringValue, GUILayout.MinWidth(Screen.width / 2));
        GUILayout.EndHorizontal();

        
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = new Color(199f / 255f, 129f / 255f, 65f / 255f);
        GUILayout.Box("Current Action", GUILayout.MinWidth(Screen.width / 4));
        GUILayout.Space(5);
        GUI.backgroundColor = Color.white;
        GUILayout.Box(currentAction.stringValue, GUILayout.MinWidth(Screen.width / 2));
        GUILayout.EndHorizontal();
        
		serializedObject.ApplyModifiedProperties ();
    }

	void Space()
	{
		GUILayout.Space(4);
		GUI.backgroundColor = Color.grey;
		GUILayout.Box("", GUILayout.Width(Screen.width-22), GUILayout.Height(2));
		GUI.backgroundColor = Color.white;
		GUILayout.Space(4);
	}
}
