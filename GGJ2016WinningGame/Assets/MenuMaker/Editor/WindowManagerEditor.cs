using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Animations;

[CustomEditor(typeof(WindowManager))]
public class WindowManagerEditor : Editor {

    WindowManager manager;

    SerializedProperty m_pages;

    SerializedObject pages;

    int currentOpenIndex = -1;
    bool opened = false;

    void OnEnable()
    {
        manager = (WindowManager)target;
        pages = new SerializedObject(manager);
        m_pages = pages.FindProperty("pages");
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        pages.Update();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical(GUILayout.Width(EditorImageLoader.iconWidth * 2));
        if (GUILayout.Button(EditorImageLoader.animDesignIcon,
                             GUILayout.Width(EditorImageLoader.iconWidth),
                             GUILayout.Height(EditorImageLoader.iconWidth)))
        {
            WindowAnimationDesigner.OpenWindow();
        }
        GUILayout.Box("Designer", GUILayout.Width(EditorImageLoader.iconWidth*2));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUILayout.Width(EditorImageLoader.iconWidth * 2));
        if (GUILayout.Button(EditorImageLoader.pagesTexture,
                             GUILayout.Width(EditorImageLoader.iconWidth),
                             GUILayout.Height(EditorImageLoader.iconWidth)))
        {
            opened = !opened;
        }
        GUILayout.Box(opened ? "Hide Pages" : "Show Pages", GUILayout.Width(EditorImageLoader.iconWidth * 2));
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUILayout.Width(EditorImageLoader.iconWidth * 2));
        if (GUILayout.Button(EditorImageLoader.addTexture,
                             GUILayout.Width(EditorImageLoader.iconWidth),
                             GUILayout.Height(EditorImageLoader.iconWidth)))
        {
            AddNewPage();
        }
        GUILayout.Box("New Page", GUILayout.Width(EditorImageLoader.iconWidth*2));
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        Space();

        if (opened)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(Screen.width - 35));
            for (int i = 0; i < m_pages.arraySize; i++)
            {
                SerializedProperty PageListRef = m_pages.GetArrayElementAtIndex(i);
                SerializedProperty Page = PageListRef.FindPropertyRelative("page");
                SerializedProperty AnimController = PageListRef.FindPropertyRelative("animController");
                SerializedProperty InAnimSpeed = PageListRef.FindPropertyRelative("inAnimSpeed");
                SerializedProperty OutAnimSpeed = PageListRef.FindPropertyRelative("outAnimSpeed");
                SerializedProperty OpenSound = PageListRef.FindPropertyRelative("openSound");
                SerializedProperty Audio = PageListRef.FindPropertyRelative("audioClip");
                SerializedProperty UseSound = PageListRef.FindPropertyRelative("useSound");
                SerializedProperty Volume = PageListRef.FindPropertyRelative("volume");
                SerializedProperty Pitch = PageListRef.FindPropertyRelative("pitch");
                SerializedProperty Loop = PageListRef.FindPropertyRelative("loop");

                bool pageActive = false;
                if (currentOpenIndex == i)
                    pageActive = true;

                string label;
                if (pageActive)
                {
                    if (Page.objectReferenceValue)
                        label = "Hide " + Page.objectReferenceValue.name;
                    else
                        label = "Hide";
                }
                else
                {
                    if (Page.objectReferenceValue)
                        label = "Show " + Page.objectReferenceValue.name;
                    else
                        label = "Show";
                }

                if (GUILayout.Button(label))
                {
                    if (currentOpenIndex != i)
                        currentOpenIndex = i;
                    else
                        currentOpenIndex = -1;
                }

                if (pageActive)
                {
                    EditorGUILayout.PropertyField(Page);
                    EditorGUILayout.PropertyField(AnimController);

                    GameObject go = (GameObject)Page.objectReferenceValue;
                    AnimatorController editorFieldController = (AnimatorController)AnimController.objectReferenceValue;


                    if (go)
                    {
                        RuntimeAnimatorController objectController = null;
                        AudioSource objectOpenSound = null;
                        //make sure the animator's 'opening' bool is set to true
                        go.GetComponent<Animator>().SetBool("Opening", true);
                        ///
                        //Animator Controller
                        ///

                        //if our reference has an animator, we can get the runtimeanimator from it, 
                        //so we may use it below
                        if (go.GetComponent<Animator>())
                            objectController = go.GetComponent<Animator>().runtimeAnimatorController;
                        //otherwise, we will add the animator and get the runtimeanimator reference
                        else
                        {
                            go.AddComponent<Animator>();
                            objectController = go.GetComponent<Animator>().runtimeAnimatorController;
                        }

                        //if editorFieldController does not exist, it means there is no value in the window manager inspector field,
                        //by default, we will set the field to the objectController, which is the actual value on the Animator component of the page
                        if (!editorFieldController)
                        {
                            AnimController.objectReferenceValue = objectController;
                        }
                        //if the editorFieldController has a value, we will manipulate the animator settings of the animator controller
                        else
                        {
                            go.GetComponent<Animator>().runtimeAnimatorController = editorFieldController;

                            InAnimSpeed.floatValue = editorFieldController.layers[0].stateMachine.states[0].state.speed;
                            OutAnimSpeed.floatValue = editorFieldController.layers[0].stateMachine.states[1].state.speed;

                            InAnimSpeed.floatValue = EditorGUILayout.Slider("In Animation Speed", InAnimSpeed.floatValue, 0.1f, 2.0f);
                            OutAnimSpeed.floatValue = EditorGUILayout.Slider("Out Animation Speed", OutAnimSpeed.floatValue, 0.1f, 2.0f);

                            editorFieldController.layers[0].stateMachine.states[0].state.speed = InAnimSpeed.floatValue;
                            editorFieldController.layers[0].stateMachine.states[1].state.speed = OutAnimSpeed.floatValue;

                        }
                        if (!go.GetComponent<WindowAnimationController>())
                        {
                            go.AddComponent<WindowAnimationController>();
                        }

                        Space();

                        ///
                        //Audio Source
                        ///

                        AudioSource editorOpenSound = (AudioSource)OpenSound.objectReferenceValue;

                        EditorGUILayout.BeginHorizontal(GUILayout.Width(Screen.width - 35));

                        EditorGUILayout.PropertyField(UseSound);
                        if (UseSound.boolValue)
                        {
                            if (GUILayout.Button(EditorImageLoader.removeTexture, GUILayout.Width(24), GUILayout.Height(24)))
                            {
                                UseSound.boolValue = false;
                                DestroyImmediate(go.GetComponent<AudioSource>(), true);
                            }
                            GUILayout.Box("Remove Audio Source");
                        }

                        EditorGUILayout.EndHorizontal();

                        if (UseSound.boolValue)
                        {
                            EditorGUILayout.PropertyField(OpenSound);
                            EditorGUILayout.PropertyField(Audio);
                        }

                        //if our reference has an audio source, we can get the audio source from it, 
                        //so we may use it below
                        if (go.GetComponent<AudioSource>())
                        {
                            objectOpenSound = go.GetComponent<AudioSource>();
                        }
                        //otherwise, we will add the audio source
                        else if (UseSound.boolValue)
                        {
                            go.AddComponent<AudioSource>();
                            objectOpenSound = go.GetComponent<AudioSource>();
                        }

                        //if editorOpenSound does not exist, it means there is no value in the window manager inspector field,
                        //by default, we will set the field to the objectOpenSound, which is the actual value on the AudioSource component of the page
                        if (!editorOpenSound && UseSound.boolValue)
                        {
                            OpenSound.objectReferenceValue = objectOpenSound;
                        }
                        //if the editorOpenSound has a value, we will manipulate the audio settings of the audio source
                        else if (UseSound.boolValue)
                        {
                            Volume.floatValue = editorOpenSound.volume;
                            Pitch.floatValue = editorOpenSound.pitch;
                            Loop.boolValue = editorOpenSound.loop;

                            if (Audio.objectReferenceValue != null)
                            {
                                go.GetComponent<AudioSource>().clip = (AudioClip)Audio.objectReferenceValue;
                                Volume.floatValue = EditorGUILayout.Slider("Volume", Volume.floatValue, 0, 1);
                                Pitch.floatValue = EditorGUILayout.Slider("Pitch", Pitch.floatValue, -3, 3);
                                EditorGUILayout.PropertyField(Loop);

                                editorOpenSound.volume = Volume.floatValue;
                                editorOpenSound.pitch = Pitch.floatValue;
                                editorOpenSound.loop = Loop.boolValue;
                            }
                        }

                    }

                    if (GUILayout.Button(EditorImageLoader.removeTexture,
                        GUILayout.Width(EditorImageLoader.iconWidth / 1.5f),
                        GUILayout.Height(EditorImageLoader.iconWidth / 1.5f)))
                    {
                        RemovePage(i);
                    }
                }
            }
            EditorGUILayout.EndVertical();

        }

        pages.ApplyModifiedProperties();
        serializedObject.ApplyModifiedProperties();
    }

    void AddNewPage()
    {
        manager.pages.Add(new WindowManager.Page());
        //Debug.Log("Added page to pages: " + m_pages.arraySize);
    }

    void RemovePage(int atIndex)
    {
        m_pages.DeleteArrayElementAtIndex(atIndex);
    }

    void Space()
    {
        GUILayout.Space(4);
        GUI.backgroundColor = Color.grey;
        GUILayout.Box("", GUILayout.Width(Screen.width - 22), GUILayout.Height(2));
        GUI.backgroundColor = Color.white;
        GUILayout.Space(4);
    }

}
