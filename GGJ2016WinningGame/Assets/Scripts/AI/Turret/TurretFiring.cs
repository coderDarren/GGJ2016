using UnityEngine;
using System.Collections;

public class TurretFiring : MonoBehaviour {

	public GameObject[] shotPos;
	public Rigidbody bullet;
	public float bulletSpeed;
	public float timer, initialTimer;
	lookAtPlayer lap;

	// Use this for initialization
	void Start () {
		lap = GetComponent<lookAtPlayer>();
		initialTimer = timer;
	}
	
	// Update is called once per frame
	void Update () {
		if(lap.playerSighted)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{	
				foreach (GameObject go in shotPos)
				{
					Rigidbody newBullet =  Instantiate(bullet,go.transform.position, go.transform.rotation) as Rigidbody;
					newBullet.velocity = transform.TransformDirection(new Vector3(0,0,bulletSpeed));
				}
				timer = initialTimer;
			}
		}
	}
}
