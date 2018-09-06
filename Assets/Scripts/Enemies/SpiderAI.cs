using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform[] Wanderpoints = new Transform[2];
    public float waitTimer;

    private int posNum;
	// Use this for initialization
	void Awake ()
    {
        
        agent = GetComponent<NavMeshAgent>();
        if (waitTimer < 0.1f)
        {
            waitTimer = 4f;
        }
    }

    private void Update()
    {
        if (posNum == 0)
        {
            agent.SetDestination(Wanderpoints[0].position); //walk to point A
        }
        else if (posNum == 1)
        {
            agent.SetDestination(Wanderpoints[1].position); //walk to point B
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == Wanderpoints[0])
        {
            posNum = 1;
        }
        if (other.transform == Wanderpoints[1])
        {
            posNum = 0;
        }
    }
}
