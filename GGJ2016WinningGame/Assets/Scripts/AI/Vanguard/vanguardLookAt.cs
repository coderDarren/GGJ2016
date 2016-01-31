using UnityEngine;
using System.Collections;

public class vanguardLookAt : MonoBehaviour {

	public GameObject player;
	public vanguardSight vs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(vs.playerSighted)
		{
			transform.LookAt(player.transform);
		}
	}
}
