using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerDamage : MonoBehaviour 
{
    void OnTriggerStay(Collider col)
    {
        if (col.name == "Player")
        {
            HealthManager.instance.health -= 0.75f;
        }
    }
}
