using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonBomb : MonoBehaviour 
{
    float speed;

    [SerializeField]
    bool huntPlayer;  //to know when to start calling bombsaway function
    bool wasHit;      //so player can't get hit twice while its invisible/exploding
    bool isExploding; //to prevent it from moving while particle effects play

    [SerializeField]
    ParticleSystem explosionParticles;

    [SerializeField]
    Transform playerTrans;
    [SerializeField]
    Vector3 targetPosition;


    [SerializeField]
    GameObject pigeonDestination;
    [SerializeField]
    GameObject[] objects;

    [SerializeField]
    SphereCollider sphereCollider;

    [SerializeField]
    Transform raycastChecker;

    [SerializeField]
    float distance;

    bool started;

	// Use this for initialization
	private void Start () 
    {
        playerTrans = GameObject.FindWithTag("Player").transform;
        speed = 14;
        huntPlayer = false;
        wasHit = false;
        isExploding = false;
        started = false;
	}

	private void Update()
	{
        RaycastHit hit;

        if (!huntPlayer)
            playerTrans = playerTrans.transform;
        if (huntPlayer) //check if the player got close to pigeon
        {
            if (!isExploding) //check if the pigeon is already exploding (stop moving if it has)
                BombsAway();
        }
        distance = Vector3.Distance(gameObject.transform.position, playerTrans.position);
        if (distance <= 18)
        {
            if (Physics.Raycast(raycastChecker.position, raycastChecker.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                Debug.DrawRay(raycastChecker.position, raycastChecker.forward);
                if (hit.transform.name == "Player")
                {
                    huntPlayer = true;
                    if (!started)
                        TargetPlayer();
                }
            }
        }
	}

	void TargetPlayer() //pick a target position
    {
        started = true;
        FaceTarget();
        targetPosition = playerTrans.position;
        Instantiate(pigeonDestination, playerTrans.position, playerTrans.rotation);
    }

	void BombsAway() //sends pigeon to target location
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, speed * Time.deltaTime);
    }

    void DeactivateRenderers() // give illusion of pigeon disappearing while particles play
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (playerTrans.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 30f);
    }

    IEnumerator SelfDestruct()
    {
        isExploding = true;
        sphereCollider.enabled = false;
        explosionParticles.Play();
        DeactivateRenderers();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Pigeon")
        {
            StartCoroutine(SelfDestruct());
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
