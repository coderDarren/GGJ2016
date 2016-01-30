using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIButton_SetScene : MonoBehaviour, IPointerClickHandler {

	public string scene = "ENTER SCENE HERE";
	public string window_state = "ENTER WINDOW STATE HERE";
    
    WindowState state;

    void Start()
    {
        state = GameObject.FindGameObjectWithTag("WindowManager").GetComponent<WindowState>();
    }
	
    public void OnPointerClick(PointerEventData ped)
    {
        state.SetState(window_state);
        SceneManager.LoadScene(scene);
    }
}
