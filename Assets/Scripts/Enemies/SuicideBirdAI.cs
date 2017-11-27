using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicideBirdAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    public Transform[] WanderPoints = new Transform[4];
    bool isHuntingPlayer = false;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        if (!player)
            Debug.Log("bird can't find player");

        if (player)
            StartCoroutine("BirdAI");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) < 4)
        {
            isHuntingPlayer = true;
            Debug.Log("hunting player true");
        }

        if (isHuntingPlayer)
        {
            agent.speed = 3.5f;
            agent.SetDestination(player.transform.position);
        }
	}

    IEnumerator BirdAI()
    {
        if (!isHuntingPlayer)
        {
            Debug.Log("BirdAI chose not to hunt player");
            agent.speed = 1.5f;
            agent.SetDestination(WanderPoints[Random.Range(0, 4)].position);
            yield return new WaitForSeconds(Random.Range(4, 7));
            if (!isHuntingPlayer)
                StartCoroutine("BirdAI");
            /*else if (isHuntingPlayer)
            {
                Debug.Log("BirdAI is now hunting player");
                agent.speed = 3.5f;
                agent.SetDestination(player.transform.position);
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 10; i++)
                {
                    agent.SetDestination(player.transform.position);
                    yield return new WaitForSeconds(1);
                }
            }*/
        }

        /*else if (isHuntingPlayer)
        {
            Debug.Log("BirdAI chose to hunt player");
            agent.speed = 3.5f;
            agent.SetDestination(player.transform.position);
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 10; i++)
            {
                agent.SetDestination(player.transform.position);
                yield return new WaitForSeconds(1);
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Bird hit Player");
            //deal damage to player, subtract steam etc
            Destroy(this.gameObject);
        }
    }
}
