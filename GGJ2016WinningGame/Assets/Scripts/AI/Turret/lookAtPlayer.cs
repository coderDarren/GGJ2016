using UnityEngine;
using System.Collections;

public class lookAtPlayer : MonoBehaviour {

	public bool playerSighted;
	public float playerHeightAdjustment;
	public GameObject player;
	public float rotationsPerMinute;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerSighted)
		{
			transform.Rotate (0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
		}
		else
		{
			transform.LookAt(new Vector3(player.transform.localPosition.x, player.transform.localPosition.y + playerHeightAdjustment, player.transform.localPosition.z));
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