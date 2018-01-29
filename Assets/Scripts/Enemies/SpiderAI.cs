using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] Wanderpoints = new Transform[10];

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("NewWanderPoint"); //start wandering
	}

    IEnumerator NewWanderPoint()
    {
        agent.SetDestination(Wanderpoints[Random.Range(0, Wanderpoints.Length)].position); //pick random wander point
        Debug.Log("wandering.. waiting");
        yield return new WaitForSeconds(Random.Range(1, 4)); //wait 6-12s
        Debug.Log("done waiting");
        StartCoroutine("NewWanderPoint"); //do it again
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            HealthManager.instance.health -= 1;
        }
        if(collision.transform.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
