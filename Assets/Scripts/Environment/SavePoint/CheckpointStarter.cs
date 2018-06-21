using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointStarter : MonoBehaviour {

    public bool trigger;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            trigger = true;
        }
        if (trigger)
        {
            XMLCheckpointManager.instance.load();
            trigger = false;
        }
    }
}
