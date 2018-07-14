using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    [SerializeField]AudioSource clinkClip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MeleeEnemy")
        {
            MeleeEnemy meleeEnemy = collision.gameObject.GetComponent<MeleeEnemy>();
            clinkClip.Play();
            Destroy(gameObject, 0.3f);
        }
        else if (collision.gameObject.tag == "RangedEnemy")
        {
            RangedEnemy rangedEnemy = collision.gameObject.GetComponent<RangedEnemy>();
            rangedEnemy.DoDamage();
            Destroy(gameObject);
        }
    }
}
