using UnityEngine;
using System.Collections;

public class CollisionDestroy : MonoBehaviour {

	public GameObject explosion;

	void OnCollision(Collision coll)
	{
		Destroy(this.gameObject);
		Instantiate(explosion, coll.transform.position,coll.transform.rotation);
	}
}
