using UnityEngine;
using System.Collections;

public class vanguardShooting : MonoBehaviour {

	public GameObject shotPos;
	public Rigidbody bullet;
	public float bulletSpeed;
	public float timer, initialTimer;
	public vanguardSight VS;

	// Use this for initialization
	void Start () {
		initialTimer = timer;
	}

	// Update is called once per frame
	void Update () {
		if(VS.playerSighted)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{	
				Rigidbody newBullet =  Instantiate(bullet,shotPos.transform.position, shotPos.transform.rotation) as Rigidbody;
				newBullet.velocity = transform.TransformDirection(new Vector3(0,0,bulletSpeed));
				timer = initialTimer;
			}
		}
	}
}
