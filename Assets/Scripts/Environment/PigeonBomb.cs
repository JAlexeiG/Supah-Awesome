using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonBomb : MonoBehaviour 
{
    float speed;
    GameObject player;

    Transform playerTrans;
    Transform targetPosition;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindWithTag("Player");
	}

	void Update()
	{
        playerTrans = playerTrans.transform;
	}

	void BombsAway()
    {
        targetPosition = playerTrans;
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, playerTrans.position, speed * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
        {
            BombsAway();
        }
	}
}
