using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int maxHealth = 100;
	float explosionRagdollForce = 100;
	int curHealth;
	GameObject explosion;


	void Start(){
		curHealth = maxHealth;
	}


	public void TakeDmg(int dmg){
		curHealth -= dmg;
		if (curHealth <= 0) {
			GameObject ragdoll = Instantiate(Resources.Load("Ragdoll Vanguard"), transform.position,
				Quaternion.identity) as GameObject;
			ragdoll.transform.Find("mixamorig:Hips").GetComponent<Rigidbody>().AddForce(Vector3.up * explosionRagdollForce, ForceMode.Impulse);
			Instantiate (explosion);

			Destroy (this.gameObject);
		}

	}

}
