using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVent : MonoBehaviour {

	Chara player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other) 
	{
        if (other.tag == "Player")
        {
            player = other.GetComponent<Chara>();
            Debug.Log(other.gameObject.name + " has stepped on " + gameObject.name);
            player.GetComponent<Rigidbody>().AddRelativeForce(0,0.2f,0,ForceMode.Acceleration);
        }
	}
}
