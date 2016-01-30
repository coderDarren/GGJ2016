using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowToolBar : MonoBehaviour {

	public GameObject window;
	public string window_name;
	bool open = true;

	void Start()
	{
		int o = PlayerPrefs.GetInt(window_name+"Active");
		if (o==0)
			open = false;
		else
			open = true;

		if (open){
			window.SetActive(true);
			PlayerPrefs.SetInt(window_name+"Active", 1);
		}
		else{
			window.SetActive(false);
			PlayerPrefs.SetInt(window_name+"Active", 0);
		}
	}

	public void SetWindow()
	{
		if (open){
			window.SetActive(false);
			PlayerPrefs.SetInt(window_name+"Active", 0);
		}
		else{
			window.SetActive(true);
			PlayerPrefs.SetInt(window_name+"Active", 1);
		}
		open = !open;
	}
}
