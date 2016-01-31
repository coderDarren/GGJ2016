using UnityEngine;
using System.Collections;

public class TriggerDestroy : MonoBehaviour {
	public GameObject explosion;

	void OnTriggerEnter(Collider coll)
	{
		Destroy(this.gameObject);
		Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
	}
}
