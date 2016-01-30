﻿using UnityEngine;
using System.Collections;

public class lookAtPlayer : MonoBehaviour {

	public bool playerSighted;
	public GameObject player;

	// Use this for initialization
	void Start () {
	
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
		if (coll.tag == "Player");
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
}
