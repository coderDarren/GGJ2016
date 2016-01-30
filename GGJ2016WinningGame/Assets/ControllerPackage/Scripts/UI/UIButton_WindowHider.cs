using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIButton_WindowHider : MonoBehaviour {

	public Sprite open_img, close_img;
	public string window_name;
	Image img;
	bool open = true;

	void Start()
	{
		img = GetComponent<Image>();
		int o = PlayerPrefs.GetInt(window_name+"Active");
		if (o==0)
			open = false;
		else
			open = true;
		
		if (open){
			img.sprite = close_img;
			PlayerPrefs.SetInt(window_name+"Active", 1);
		}
		else{
			img.sprite = open_img;
			PlayerPrefs.SetInt(window_name+"Active", 0);
		}
	}

	public void SetImage()
	{
		if (open)
		{
			img.sprite = open_img;
			PlayerPrefs.SetInt(window_name+"Active", 0);
		}
		else
		{
			img.sprite = close_img;
			PlayerPrefs.SetInt(window_name+"Active", 1);
		}
		open = !open;
	}
}
