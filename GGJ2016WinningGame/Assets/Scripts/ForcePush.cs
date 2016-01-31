using UnityEngine;
using System.Collections;

public class ForcePush : MonoBehaviour {
    public ParticleSystem system;
    public float forceAmount;
    private ParticleSystem.EmissionModule emission;

    public float distance;
    // Use this for initialization
    void Start () {
        //system = GetComponent<ParticleSystem>();
        emission = system.emission;
        
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if(Input.GetButtonDown("Force"))
        {
            emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(2.0f, 100) });

            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, distance))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    GameObject ragdoll = Instantiate(Resources.Load("Ragdoll Vanguard"), hit.transform.position,
                        Quaternion.identity) as GameObject;
                    Destroy(hit.transform.gameObject);
                    ragdoll.transform.Find("mixamorig:Hips").GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * forceAmount, ForceMode.Impulse);
                    //ragdoll.GetComponent<Rigidbody>().AddForce(cam.transform.forward * forceAmount);
                    //foreach(Transform t in ragdoll.transform)
                    //{
                    //t.position += Vector3.forward * 5;
                    //if(t.GetComponent<Rigidbody>() != null)
                    //{
                    //t.GetComponent<Rigidbody>().AddForce(cam.transform.forward * forceAmount, ForceMode.Impulse);
                    //}
                    //}
                }
            }
        }
    }
}
