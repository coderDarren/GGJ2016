using UnityEngine;
using System.Collections;

public class ThrowStaaas : MonoBehaviour {
    private Vector3 startPos;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        startPos += Vector3.forward * 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Star Throw"))
        {
            Instantiate(Resources.Load("Ninja Star"), startPos, Quaternion.identity);
        }
	}
}
