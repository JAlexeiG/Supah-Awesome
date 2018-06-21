using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVent : Power {

	Chara player;
    [SerializeField]
    float strength;
    Collider m_collider;
    public ParticleSystem steam;

    // Use this for initialization
    void Start () {
        m_collider = GetComponent<Collider>();
        m_collider.enabled = isPowered;
    }

    // Update is called once per frame
    void Update()
    {
        m_collider.enabled = isPowered;
        if (isPowered)
        {
            steam.Play();
        }
        else
        {
            steam.Stop();
        }
    }
    void OnTriggerStay(Collider other) 
	{
        if (other.tag == "Player")
        {
            player = other.GetComponent<Chara>();
            Debug.Log(other.gameObject.name + " has stepped on " + gameObject.name);
            //player.GetComponent<Rigidbody>().AddExplosionForce(strength, transform.position ,strength);
            player.GetComponent<Rigidbody>().AddRelativeForce(transform.up * strength, ForceMode.Acceleration);
        }
	}
}
