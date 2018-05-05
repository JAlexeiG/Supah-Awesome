using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMele : MonoBehaviour
{
    [SerializeField]
    private float hitForce;
    [SerializeField]
    private Transform playerTrans;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MeleeEnemy")
        {
            other.GetComponent<MeleeEnemy>().DoDamage();
            other.GetComponent<MeleeEnemy>().getHit(true);
            other.GetComponent<Rigidbody>().AddExplosionForce(hitForce, playerTrans.position, 300,2);
            Debug.Log("MeleEnemyhit");
        }
    }
}
