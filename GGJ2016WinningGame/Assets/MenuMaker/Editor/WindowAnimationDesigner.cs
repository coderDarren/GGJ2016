using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Animations;

public class WindowAnimationDesigner : EditorWindow {

    //[MenuItem("Window/Window Animation Designer")]
	public static void OpenWindow()
    {
        EditorWindow.GetWindow(typeof(WindowAnimationDesigner));
    }

    AnimationClip inAnimation;
    AnimationClip outAnimation;
    string controllerName = "";
    bool animationsInPlace = false;
    float inAnimSpeed = 1;
    float outAnimSpeed = 1;


    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 2000, 2000), EditorImageLoader.designerTexture);

        EditorGUILayout.BeginHorizontal();
        if (!animationsInPlace)
            EditorGUILayout.HelpBox("To create a new controller for your page, add the 'in' and 'out' animations.", MessageType.Info);
        EditorGUILayout.EndHorizontal();

        //in animation fields
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
        GUILayout.Box("In Animation", GUILayout.Width(100));
        inAnimation = EditorGUILayout.ObjectField(inAnimation, typeof(AnimationClip), false) as AnimationClip;
        EditorGUILayout.EndHorizontal();

        if (inAnimation != null)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
            GUILayout.Box("In Speed", GUILayout.Width(100));
            inAnimSpeed = EditorGUILayout.Slider(inAnimSpeed, 0.1f, 2.0f);
            EditorGUILayout.EndHorizontal();
        }

        //out animation fields
        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
        GUILayout.Box("Out Animation", GUILayout.Width(100));
        outAnimation = EditorGUILayout.ObjectField(outAnimation, typeof(AnimationClip), false) as AnimationClip;
        EditorGUILayout.EndHorizontal();

        if (outAnimation != null)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
            GUILayout.Box("Out Speed", GUILayout.Width(100));
            outAnimSpeed = EditorGUILayout.Slider(outAnimSpeed, 0.1f, 2.0f);
            EditorGUILayout.EndHorizontal();
        }
        
        //naming animator
        if (inAnimation != null &&
            outAnimation != null)
        {
            animationsInPlace = true;
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
            GUILayout.Box("Animator Controller Name", GUILayout.Width(100));
            controllerName = EditorGUILayout.TextField(controllerName);
            EditorGUILayout.EndHorizontal();
        }
        else
            animationsInPlace = false;

        //creating animator
        if (controllerName.Length > 0)
        {
            if (GUILayout.Button("Create Animator Controller", GUILayout.MaxWidth(350)))
            {
                CreateAnimatorController(inAnimation, outAnimation, controllerName);
            }
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
            EditorGUILayout.HelpBox("Your shiny new animator controller will be placed inside PageAnimations->Controllers.", MessageType.Info);
            EditorGUILayout.EndHorizontal();
        }
    }

    void CreateAnimatorController(AnimationClip inAnim, AnimationClip outAnim, string animName)
    {
        AnimatorController rac = AnimatorController.CreateAnimatorControllerAtPath("Assets/WindowStateManagement/PageAnimations/Controllers/"+ animName + ".controller");

        //get state machine
        var stateMachine = rac.layers[0].stateMachine;

        //create parameters
        rac.AddParameter("Opening", AnimatorControllerParameterType.Bool);

        //add states
        stateMachine.AddState(inAnim.name);
        stateMachine.AddState(outAnim.name);

        //place clips into states
        AnimatorState inState = stateMachine.states[0].state;
        inState.motion = inAnim;
        inState.speed = inAnimSpeed;

        AnimatorState outState = stateMachine.states[1].state;
        outState.motion = outAnim;
        outState.speed = outAnimSpeed;

        //create transitions
        var inStateExitTransition = inState.AddTransition(outState);
        inStateExitTransition.AddCondition(AnimatorConditionMode.IfNot, 0, "Opening");
    }
}
