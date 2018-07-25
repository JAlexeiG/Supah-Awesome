using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaParticles : MonoBehaviour {

    /// <summary>
    /// Gets particles emited from the Tesla Coil and directs them towards the player
    /// </summary>
    
    private ParticleSystem pSystem;

    private ParticleSystem.Particle[] particles;

    private Transform playerTransform;

    [SerializeField]
    private float velocityForce;

	// Use this for initialization
	void Start () {
        pSystem = GetComponent<ParticleSystem>();

        playerTransform = FindObjectOfType<Chara>().transform;
	}
	
	// Update is called once per frame
	void Update () {

        particles = new ParticleSystem.Particle[pSystem.particleCount];

        pSystem.GetParticles(particles);

        int totalParticles = pSystem.GetParticles(particles);

        Debug.Log(totalParticles);

        for(int i = 0; i < totalParticles;i++)
        {
            Vector3 normalVelocity = (playerTransform.position - particles[i].position).normalized;
            
            particles[i].velocity += normalVelocity * velocityForce;
        }

        pSystem.SetParticles(particles, totalParticles);

    }
}
