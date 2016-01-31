using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {
    
    private ParticleSystem system;
    public GameObject player;
    public float wait;
    public float speed;
    public float lifetime;
    public float xpStart;
    public XP xpScript;

    private bool gainXp;
    private ParticleSystem.Particle[] particles;
    private float currentTime;
    private ParticleSystem.EmissionModule emission;

	// Use this for initialization
	void Start () {
        currentTime = 0.0f;
        gainXp = false;
        system = GetComponent<ParticleSystem>();

        emission = system.emission;
        emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(2.0f, 100) });
        particles = new ParticleSystem.Particle[system.maxParticles];
    }
	
	// Update is called once per frame
	void Update () {
	    if(currentTime >= wait && currentTime < lifetime)
        {
            int liveParticles = system.GetParticles(particles);
            for(int i = 0; i < liveParticles; i++)
            {
                particles[i].position = Vector3.MoveTowards(particles[i].position, 
                    player.transform.position, speed * Time.deltaTime);
            }

            system.SetParticles(particles, liveParticles);
        }

        if(currentTime >= xpStart && gainXp == false)
        {
            gainXp = true;
            xpScript.IncrementXp();
        }

        if (currentTime > lifetime)
        {
            Destroy(gameObject);
        }

        currentTime += Time.deltaTime;
	}
}
