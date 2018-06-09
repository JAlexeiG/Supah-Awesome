using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {

    XMLCheckpointManager manager;

    
    [SerializeField]
    Checkpoints nextCheckpoint;

    private bool hasNextPoint;
	// Use this for initialization
	void Start () {
        manager = XMLCheckpointManager.instance;
        if (nextCheckpoint)
        {
            hasNextPoint = true;
        }
	}

    private void Update()
    {
        if(!nextCheckpoint && hasNextPoint)
        {
            Destroy(gameObject);
        }
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
