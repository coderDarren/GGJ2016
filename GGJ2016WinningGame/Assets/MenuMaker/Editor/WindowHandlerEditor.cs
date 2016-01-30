using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor (typeof (WindowStateHandler))]
[CanEditMultipleObjects]
public class WindowHandlerEditor : Editor  {

    WindowStateHandler ab;
    
    SerializedProperty m_states;
    SerializedObject stateTarget;
    
    string expandedState;
    string expandedAction;
    string expandedChild;
    bool displayStates = false;
    bool displayActions = false;

    void OnEnable()
    {
        ab = (WindowStateHandler)target;
        stateTarget = new SerializedObject(ab);
        m_states = stateTarget.FindProperty("states");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        stateTarget.Update();

        EditorGUILayout.BeginHorizontal();

        
            
        EditorGUILayout.BeginVertical(GUILayout.Width(EditorImageLoader.iconWidth * 2));
		if (GUILayout.Button(EditorImageLoader.statesIcon,
                                GUILayout.Width(EditorImageLoader.iconWidth),
                                GUILayout.Height(EditorImageLoader.iconWidth)))
        {
            displayStates = !displayStates;
        }
        GUILayout.Box(displayStates?"Hide States":"Show States", GUILayout.Width(EditorImageLoader.iconWidth*2));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUILayout.Width(EditorImageLoader.iconWidth * 2));
        if (GUILayout.Button(EditorImageLoader.addTexture,
                             GUILayout.Width(EditorImageLoader.iconWidth),
                             GUILayout.Height(EditorImageLoader.iconWidth)))
        {
            m_states.InsertArrayElementAtIndex(m_states.arraySize);
            SerializedProperty MyListRef = m_states.GetArrayElementAtIndex(m_states.arraySize-1);
            SerializedProperty StateName = MyListRef.FindPropertyRelative("stateName");
            SerializedProperty m_actions = MyListRef.FindPropertyRelative("possibleActions");
            SerializedProperty StateNameSaved = MyListRef.FindPropertyRelative("nameSaved");
            StateName.stringValue = "";
            m_actions.ClearArray();
            StateNameSaved.boolValue = false;
        }
        GUILayout.Box("Add State", GUILayout.Width(EditorImageLoader.iconWidth * 2));
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        Space();
            
        if (displayStates)
        {
            //States List
            for (int i = 0; i < m_states.arraySize; i++)
            {
                SerializedProperty MyListRef = m_states.GetArrayElementAtIndex(i);
                SerializedProperty StateName = MyListRef.FindPropertyRelative("stateName");
                SerializedProperty m_actions = MyListRef.FindPropertyRelative("possibleActions");
                SerializedProperty StateNameSaved = MyListRef.FindPropertyRelative("nameSaved");

                EditorGUILayout.BeginHorizontal(GUILayout.Width(Screen.width / 1.1f));
                if (!StateNameSaved.boolValue)
                {
                    EditorGUILayout.PropertyField(StateName);
                    if (GUILayout.Button("Save state name."))
                    {
                        if (StateName.stringValue != "")
                        {
                            StateNameSaved.boolValue = true;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();

                //Set expanded state
                if (GUILayout.Button(StateName.stringValue, GUILayout.Height(20)))
                {
                    if (expandedState != StateName.stringValue)
                        expandedState = StateName.stringValue;
                    else
                        expandedState = "";
                }
                
                GUI.backgroundColor = Color.white;

                //Check expanded state
                if (expandedState == StateName.stringValue && StateNameSaved.boolValue)
                {
                    if (StateNameSaved.boolValue)
                    {
                        if (GUILayout.Button("Rename state.", GUILayout.Width(Screen.width / 2)))
                        {
                            StateNameSaved.boolValue = false;
                        }
                    }

                    EditorGUILayout.BeginHorizontal(GUILayout.Width(Screen.width / 2));

                    GUI.backgroundColor = new Color(199f / 255f, 129f / 255f, 65f / 255f);
                    if (GUILayout.Button(displayActions ? "Hide Derived Actions " : "Show Derived Actions",
                                            GUILayout.Width(200),
                                            GUILayout.Height(24)))
                    {
                        displayActions = !displayActions;
                    }

                    GUI.backgroundColor = Color.white;

                    if (GUILayout.Button(EditorImageLoader.addTexture,
                                         GUILayout.Width(EditorImageLoader.iconWidth/1.5f),
                                         GUILayout.Height(EditorImageLoader.iconWidth/1.5f)))
                    {
                        m_actions.InsertArrayElementAtIndex(m_actions.arraySize);
                        m_actions.GetArrayElementAtIndex(m_actions.arraySize - 1).FindPropertyRelative("actionName").stringValue = "";
                    }

                    EditorGUILayout.EndHorizontal();
                    
                    if (displayActions)
                    {
                        //States' Actions List
                        for (int j = 0; j < m_actions.arraySize; j++)
                        {
                            SerializedProperty MyList = m_actions.GetArrayElementAtIndex(j);
                            SerializedProperty ActionName = MyList.FindPropertyRelative("actionName");
                            SerializedProperty Continuous = MyList.FindPropertyRelative("continuouslyInvokeAction");
                            SerializedProperty UpdateStyle = MyList.FindPropertyRelative("updateFunction");
                            SerializedProperty UseUnityEvent = MyList.FindPropertyRelative("useUnityEvent");
                            SerializedProperty StartAction = MyList.FindPropertyRelative("StartAction");
                            SerializedProperty UseWindowEvent = MyList.FindPropertyRelative("useWindowEvent");
                            SerializedProperty WindowEvent = MyList.FindPropertyRelative("windowAction");
                            SerializedProperty ResourceFolder = MyList.FindPropertyRelative("resourceFolder");
                            SerializedProperty NewPageName = MyList.FindPropertyRelative("newPageName");
                            SerializedProperty BasePageName = MyList.FindPropertyRelative("basePageName");
                            SerializedProperty RemoveAllPages = MyList.FindPropertyRelative("removeAllPages");
                            SerializedProperty ActionNameSaved = MyList.FindPropertyRelative("nameSaved");

                            EditorGUILayout.BeginHorizontal(GUILayout.Width(Screen.width / 1.1f));
                            if (!ActionNameSaved.boolValue)
                            {
                                EditorGUILayout.PropertyField(ActionName);
                                if (GUILayout.Button("Save action name."))
                                {
                                    if (ActionName.stringValue != "")
                                    {
                                        ActionNameSaved.boolValue = true;
                                    }
                                }
                            }
                            EditorGUILayout.EndHorizontal();

                            if (GUILayout.Button(ActionName.stringValue, GUILayout.Width(200)))
                            {
                                if (expandedAction != ActionName.stringValue)
                                    expandedAction = ActionName.stringValue;
                                else
                                    expandedAction = "";
                            }

                            if (expandedAction == ActionName.stringValue && ActionNameSaved.boolValue)
                            {
                                if (ActionNameSaved.boolValue)
                                {
                                    if (GUILayout.Button("Rename action.", GUILayout.Width(Screen.width / 2)))
                                    {
                                        ActionNameSaved.boolValue = false;
                                    }
                                }

                                EditorGUILayout.PropertyField(UseUnityEvent);
                                if (UseUnityEvent.boolValue)
                                {
                                    EditorGUILayout.PropertyField(StartAction);
                                }
                                EditorGUILayout.PropertyField(UseWindowEvent);
                                if (UseWindowEvent.boolValue)
                                {
                                    Space();
                                    EditorGUILayout.PropertyField(WindowEvent);
                                    EditorGUILayout.PropertyField(ResourceFolder);
                                    EditorGUILayout.PropertyField(NewPageName);
                                    EditorGUILayout.PropertyField(BasePageName);
                                    EditorGUILayout.PropertyField(RemoveAllPages);
                                    Space();
                                }

                                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(300));
                                EditorGUILayout.PropertyField(Continuous);
                                if (!Continuous.boolValue)
                                    EditorGUILayout.HelpBox("Set to true if you want these events to be invoked in update.\n" +
                                                            "Otherwise, the events will be invoked once.", MessageType.Info);
                                EditorGUILayout.EndHorizontal();
                                if (Continuous.boolValue)
                                {
                                    EditorGUILayout.PropertyField(UpdateStyle);
                                }

                                EditorGUILayout.BeginHorizontal();
                                if (GUILayout.Button(EditorImageLoader.removeTexture,
                                    GUILayout.Width(EditorImageLoader.iconWidth / 1.5f),
                                    GUILayout.Height(EditorImageLoader.iconWidth / 1.5f)))
                                {
                                    m_actions.DeleteArrayElementAtIndex(j);
                                }
                                GUILayout.Box("Delete Action");
                                EditorGUILayout.EndHorizontal();
                            }
                            Space();
                        }
                    }

                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button(EditorImageLoader.removeTexture,
                        GUILayout.Width(EditorImageLoader.iconWidth / 1.5f),
                        GUILayout.Height(EditorImageLoader.iconWidth / 1.5f)))
                    {
                        m_states.DeleteArrayElementAtIndex(i);
                    }
                    GUILayout.Box("Delete State");
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        

        stateTarget.ApplyModifiedProperties();
        serializedObject.ApplyModifiedProperties();
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