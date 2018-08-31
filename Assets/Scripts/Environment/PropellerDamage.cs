using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerDamage : MonoBehaviour 
{
    void OnTriggerStay(Collider col)
    {
        if (col.name == "Player")
        {
            HealthManager.instance.health -= 0.5f;
        }
        
        else if (col.gameObject.tag == "MeleeEnemy")
        {
            col.GetComponent<MeleeEnemy>().DoDamage();
        }
    }
}
