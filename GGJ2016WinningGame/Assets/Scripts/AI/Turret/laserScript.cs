using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {

	LineRenderer laser;
	public bool laserOn = true;
	public float laserTexRot = 2;
	Renderer laserTex;
	Light laserLight;

	void Start () 
	{
		laserOn = true;
		laser = gameObject.GetComponent<LineRenderer>();
		laser.enabled = true;
		laserTex = laser.GetComponent<Renderer>();
		laserTex.enabled = true;
		laserLight = laser.GetComponent<Light>();
		laserLight.enabled = true;
	}

	void Update () 
	{
		if (laserOn)
		{
			StopCoroutine("FireLaser");
			StartCoroutine("FireLaser");
		}
	}

	IEnumerator FireLaser ()
	{
		laser.enabled = true;
		laserTex.enabled = true;
		laserLight.enabled = true;

		while (laserOn = true)
		{
			laser.material.mainTextureOffset = new Vector2(0, Time.time * laserTexRot);
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			laser.SetPosition(0, ray.origin);
			if(Physics.Raycast(ray, out hit, 100))
			{
				laser.SetPosition(1, hit.point);
			}
			else
			{
				laser.SetPosition(1, ray.GetPoint(100));
			}

			yield return null;
		}

		laserOn = false;
		laser.enabled = false;
		laserTex.enabled = false;
	}
}