using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    [SerializeField] GameObject rangedEnemy;
    [SerializeField] GameObject meleeEnemy;
    [SerializeField] GameObject spider;

    [SerializeField] Transform[] rangedSpawns;
    [SerializeField] Transform[] meleeSpawns;
    [SerializeField] Transform[] spiderSpawns;
    [SerializeField] float Timer = 3f;    

    Transform player;
    Vector3 playerLocation;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("CheckRanged");
        StartCoroutine("CheckMelee");
        StartCoroutine("CheckSpider");
	}
	
	// Update is called once per frame
	void Update () 
    {
        playerLocation = player.position;
	}

    IEnumerator CheckRanged()
    {
        for (int i = 0; i < rangedSpawns.Length; i++)
        {
            if (Vector3.Distance(playerLocation, rangedSpawns[i].position) > 50)
            {
                Destroy(rangedSpawns[i].GetChild(0));
            }
            else if (Vector3.Distance(playerLocation, rangedSpawns[i].position) < 50)
            {
                GameObject spawn = Instantiate(rangedEnemy, rangedSpawns[i].position, rangedSpawns[i].rotation);
                spawn.transform.SetParent(rangedSpawns[i]);
            }
        }
        yield return new WaitForSeconds(Timer);
        StartCoroutine("CheckRanged");
    }

    IEnumerator CheckMelee()
    {
        for (int i = 0; i < meleeSpawns.Length; i++)
        {
            if (Vector3.Distance(playerLocation, meleeSpawns[i].position) > 50)
            {
                Destroy(meleeSpawns[i].GetChild(0));
            }
            else if (Vector3.Distance(playerLocation, meleeSpawns[i].position) < 50)
            {
                GameObject spawn = Instantiate(meleeEnemy, meleeSpawns[i].position, meleeSpawns[i].rotation);
                spawn.transform.SetParent(meleeSpawns[i]);
            }
        }
        yield return new WaitForSeconds(Timer);
        StartCoroutine("CheckMelee");
    }

    IEnumerator CheckSpider()
    {
        for (int i = 0; i < spiderSpawns.Length; i++)
        {
            if (Vector3.Distance(playerLocation, spiderSpawns[i].position) > 50)
            {
                Destroy(spiderSpawns[i].GetChild(0));
            }
            else if (Vector3.Distance(playerLocation, spiderSpawns[i].position) < 50)
            {
                GameObject spawn = Instantiate(spider, spiderSpawns[i].position, spiderSpawns[i].rotation);
                spawn.transform.SetParent(spiderSpawns[i]);
            }
        }
        yield return new WaitForSeconds(Timer);
        StartCoroutine("CheckSpider");
    }
}
