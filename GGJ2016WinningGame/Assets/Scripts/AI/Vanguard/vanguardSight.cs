using UnityEngine;
using System.Collections;

public class vanguardSight : MonoBehaviour {

	public bool playerSighted;
	public float heightMultiplier;
	public float sightDist;

	public Animator animator;
	
	// Update is called once per frame
	void Update () {
		if(!playerSighted)
		{					
			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right) * sightDist, Color.green);
			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right) * sightDist, Color.green);
			RaycastHit hit;
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))				
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					animator.SetBool("playerSpotted", true);
					playerSighted = true;
				}
			}
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right), out hit, sightDist))				
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					animator.SetBool("playerSpotted", true);
					playerSighted = true;
				}
			}
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right), out hit, sightDist))				
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					animator.SetBool("playerSpotted", true);
					playerSighted = true;
				}
			}
		}
		if (playerSighted)
		{
			animator.SetBool("playerSpotted", true);
		}
	}
}