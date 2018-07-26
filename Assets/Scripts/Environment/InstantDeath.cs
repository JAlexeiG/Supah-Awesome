using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthManager.instance.health = 0;
        }
    }

    public void KillPlayer()
    {
        HealthManager.instance.health = 0;
    }
}
