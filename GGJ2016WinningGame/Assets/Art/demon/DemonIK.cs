using UnityEngine;
using System.Collections;

public class DemonIK : MonoBehaviour {

	Animator anim;

	public Transform player;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void OnAnimatorIK(){
		anim.SetLookAtPosition(player.position);       
		anim.SetLookAtWeight(1.0f);
	}
}
