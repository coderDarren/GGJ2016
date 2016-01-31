using UnityEngine;
using System.Collections;

public class NinjaStaaaas : MonoBehaviour {
    public float rotationSpeed;
    public float moveSpeed;
    public Camera cam;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        transform.forward = cam.transform.forward;
        rb.AddForce(cam.transform.forward * moveSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = Quaternion.Euler(rb.transform.localEulerAngles * rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * rotation);
	}
}
