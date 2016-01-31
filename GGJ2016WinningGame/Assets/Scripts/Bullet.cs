using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
	MeshRenderer myBullet;
	public float bulletTexRot;
	// Use this for initialization
	void Start () {
		myBullet = GetComponent<MeshRenderer>();
	}

	void OnCollisionEnter(Collision coll)
	{
		Destroy(this.gameObject);
		Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
	}

	// Update is called once per frame
	void Update () {
		myBullet.material.mainTextureOffset = new Vector2(0, Time.time * bulletTexRot);
	}
}
