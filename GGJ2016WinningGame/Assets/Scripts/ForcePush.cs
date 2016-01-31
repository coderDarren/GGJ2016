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
        system.Stop();
        
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if(Input.GetButtonDown("Force"))
        {
            emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.5f, 100) });
            system.Play();

            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, distance))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    GameObject ragdoll = Instantiate(Resources.Load("Ragdoll Vanguard"), hit.transform.position,
                        Quaternion.identity) as GameObject;
                    Destroy(hit.transform.gameObject);
                    ragdoll.transform.Find("mixamorig:Hips").GetComponent<Rigidbody>().AddForce(
                        Camera.main.transform.forward * forceAmount, ForceMode.Impulse);
                }
            }
        }
    }
}
