﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] Wanderpoints = new Transform[2];
    public float wanderTimer;
    
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        if (wanderTimer < 0.1f)
        {
            wanderTimer = 4f;
        }
	}
    
    IEnumerator NewWanderPoint()
    {
        yield return new WaitForSeconds(0.1f); //ensure wander points are locked in
        agent.SetDestination(Wanderpoints[0].position); //walk to point A
        yield return new WaitForSeconds(wanderTimer); //wait
        agent.SetDestination(Wanderpoints[1].position); //walk to point B
        yield return new WaitForSeconds(wanderTimer); //wait
        StartCoroutine("ContinueWandering"); //repeat
    }

    IEnumerator ContinueWandering()
    {
        agent.SetDestination(Wanderpoints[0].position); //walk to point A
        yield return new WaitForSeconds(wanderTimer); //wait
        agent.SetDestination(Wanderpoints[1].position); //walk to point B
        yield return new WaitForSeconds(wanderTimer); //wait
        StartCoroutine("ContinueWandering"); //repeat
    }

    public void setPos(Transform pos1, Transform pos2)
    {
        Wanderpoints[0] = pos1;
        Wanderpoints[1] = pos2;
    }
    public void start()
    {
        StartCoroutine("NewWanderPoint"); //start wandering
    }
}
