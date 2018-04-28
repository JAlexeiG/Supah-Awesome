using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMele : MonoBehaviour
{
    [SerializeField]
    private float hitForce;
    [SerializeField]
    private float hitRadius;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MeleeEnemy")
        {
            other.GetComponent<MeleeEnemy>().DoDamage();
            other.GetComponent<Rigidbody>().AddExplosionForce(hitForce, transform.position, hitRadius);
            Debug.Log("MeleEnemyhit");
        }
    }
}
