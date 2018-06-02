using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {

    XMLCheckpointManager manager;

	// Use this for initialization
	void Start () {
        manager = XMLCheckpointManager.instance;
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            manager.save();
            Destroy(gameObject);
        }
    }
}
