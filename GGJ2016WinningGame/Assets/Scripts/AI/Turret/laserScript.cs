using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {

	LineRenderer laser;
	public bool laserOn = false;
	public float laserTexRot = 2;
	Renderer laserTex;
	Light laserLight;

	void Start () 
	{
		laser = gameObject.GetComponent<LineRenderer>();
		laser.enabled = false;
		laserTex = laser.GetComponent<Renderer>();
		laserTex.enabled = false;
		laserLight = laser.GetComponent<Light>();
		laserLight.enabled = false;
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
