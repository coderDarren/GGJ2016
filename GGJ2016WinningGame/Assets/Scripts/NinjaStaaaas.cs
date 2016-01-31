using UnityEngine;
using System.Collections;

public class NinjaStaaaas : MonoBehaviour {
    public float rotationSpeed;
    public float moveSpeed;
    private Rigidbody rb;

    float lifeTimer = 0;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        transform.forward = Camera.main.transform.forward;
        rb.AddForce(Camera.main.transform.forward * moveSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = Quaternion.Euler(rb.transform.localEulerAngles * rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * rotation);
        lifeTimer += Time.deltaTime;
        if (lifeTimer > 5)
            Destroy(gameObject);
	}
}
