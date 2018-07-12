using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] Wanderpoints = new Transform[2];

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("NewWanderPoint"); //start wandering
	}

    IEnumerator NewWanderPoint()
    {
        yield return new WaitForSeconds(0.1f); //ensure wander points are locked in
        agent.SetDestination(Wanderpoints[0].position); //walk to point A
        yield return new WaitForSeconds(2); //wait 3s
        agent.SetDestination(Wanderpoints[1].position); //walk to point B
        yield return new WaitForSeconds(2); //wait 3s
        StartCoroutine("ContinueWandering"); //repeat
    }

    IEnumerator ContinueWandering()
    {
        agent.SetDestination(Wanderpoints[0].position); //walk to point A
        yield return new WaitForSeconds(2); //wait 3s
        agent.SetDestination(Wanderpoints[1].position); //walk to point B
        yield return new WaitForSeconds(2); //wait 3s
        StartCoroutine("ContinueWandering"); //repeat
    }
}
