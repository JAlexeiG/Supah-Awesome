using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyProjectile : MonoBehaviour
{
    float lifetime = 1f;
    float damage = 5;
    
	// Use this for initialization
	void Start ()
    {
		Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthManager.instance.health -= damage;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
