using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] Wanderpoints = new Transform[4];

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("NewWanderPoint"); //start wandering
	}

    IEnumerator NewWanderPoint()
    {
        yield return new WaitForSeconds(0.1f); //ensure wander points are locked in
        agent.SetDestination(Wanderpoints[Random.Range(0, 4)].position); //pick random wander point
        //Debug.Log("wandering.. waiting");
        yield return new WaitForSeconds(Random.Range(4, 10)); //wait 4-9s
        //Debug.Log("done waiting");
        StartCoroutine("NewWanderPoint"); //do it again
    }
}
