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
		player = other.GetComponent<Chara> ();
		Debug.Log (other.gameObject.name + " has stepped on " + gameObject.name);
        player.moveDirection.y += 0.2f;
	}
}
