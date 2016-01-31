using UnityEngine;
using System.Collections;

public class vanguardSight : MonoBehaviour {

	public bool playerSighted;
	public float heightMultiplier;
	public float sightDist;

	public Animator animator;

	// Use this for initialization
	void Start () {
		
	}
	
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
					playerSighted = true;
				}
			}
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right), out hit, sightDist))				
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					playerSighted = true;
				}
			}
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right), out hit, sightDist))				
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					playerSighted = true;
				}
			}
		}
	}
}
