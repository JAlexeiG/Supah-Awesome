using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MeleeEnemy")
        {
            MeleeEnemy meleeEnemy = collision.gameObject.GetComponent<MeleeEnemy>();
            meleeEnemy.DoDamage();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "RangedEnemy")
        {
            RangedEnemy rangedEnemy = collision.gameObject.GetComponent<RangedEnemy>();
            rangedEnemy.DoDamage();
            Destroy(gameObject);
        }
    }
}
