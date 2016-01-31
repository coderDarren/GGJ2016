using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton_SetWindow : MonoBehaviour, IPointerClickHandler {
      
    public string window_state = "ENTER STATE HERE";
    
    WindowState state;
    Button b;
    string stateName;

    void Start()
    {
        b = GetComponent<Button>();
        state = GameObject.FindGameObjectWithTag("WindowManager").GetComponent<WindowState>();
    }
	
    public void OnPointerClick(PointerEventData ped)
    {
        if (b != null)
        {
            if (b.interactable)
            {
                state.SetState(window_state);
            }
        }
        else { 
            state.SetState(window_state);
        }
    }
}
