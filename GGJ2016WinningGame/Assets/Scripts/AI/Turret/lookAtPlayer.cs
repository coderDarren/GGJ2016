using UnityEngine;
using System.Collections;

public class lookAtPlayer : MonoBehaviour {

	public bool playerSighted;
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (playerSighted)
		{
			transform.LookAt(player.transform);
		}
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Player")
		{
			if (player.GetComponent<PlayerController>().crouch)
			{
				
			}
			else
			{
				playerSighted = true;	
			}				
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "Player")
		{
			playerSighted = false;
		}
			
	}
}
