using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointStarter : MonoBehaviour {

    public bool trigger;
    private void Update()
    {
        if (trigger)
        {
            XMLCheckpointManager.instance.load();
            trigger = false;
        }
    }
}
