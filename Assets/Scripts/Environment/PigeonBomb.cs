using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonBomb : MonoBehaviour 
{
    float speed;

    [SerializeField]
    bool huntPlayer;
    bool wasHit;    //so player can't 
    bool isExploding; //variable to prevent it from moving while particle effects play

    [SerializeField]
    ParticleSystem explosionParticles;

    [SerializeField]
    Transform playerTrans;
    [SerializeField]
    Transform targetPosition;

    [SerializeField]
    GameObject[] objects;

	// Use this for initialization
	void Start () 
    {
        playerTrans = GameObject.FindWithTag("Player").transform;
        speed = 6;
        huntPlayer = false;
        wasHit = false;
        isExploding = false;
	}

	void Update()
	{
        if (!huntPlayer)
            playerTrans = playerTrans.transform;
        if (huntPlayer)
        {
            if (!isExploding)
                BombsAway();
        }
	}

    void TargetPlayer()
    {
        targetPosition = playerTrans;
    }

	void BombsAway()
    {
        Debug.Log("fucking chase him you stupid cunt");
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerTrans.position, speed * Time.deltaTime);
    }

    void DeactivateRenderers()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

    IEnumerator SelfDestruct()
    {
        isExploding = true;
        explosionParticles.Play();
        DeactivateRenderers();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
        {
            TargetPlayer();
            huntPlayer = true;
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
        {
            if (!wasHit)
            {
                StartCoroutine(SelfDestruct());
                HealthManager.instance.health -= 20;
                wasHit = true;
            }
        }

        else
        {
            StartCoroutine(SelfDestruct());
        } 
	}
}
