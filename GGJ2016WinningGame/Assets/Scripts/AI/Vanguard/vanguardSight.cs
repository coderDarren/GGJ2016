using UnityEngine;
using System.Collections;

public class vanguardSight : MonoBehaviour {

	public bool playerSighted;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!playerSighted)
		{					
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 15))
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					playerSighted = true;
				}
			}
		}
	}
}
