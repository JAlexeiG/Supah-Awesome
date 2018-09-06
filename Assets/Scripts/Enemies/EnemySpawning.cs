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

    [SerializeField] bool[] meleeSpawned;
    [SerializeField] bool[] rangedSpawned;
    [SerializeField] bool[] spiderSpawned;

    Transform player;
    Vector3 playerLocation;

    /*public struct EnemySpawnData
    {
        public bool isDead;
        public Vector3 position;
    }*/

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("CheckRanged");
        StartCoroutine("CheckMelee");
        StartCoroutine("CheckSpider");

        for (int i = 0; i > meleeSpawned.Length; i++)
        {
            meleeSpawned[i] = false;
        }

        for (int i = 0; i > rangedSpawned.Length; i++)
        {
            rangedSpawned[i] = false;
        }

        for (int i = 0; i > spiderSpawned.Length; i++)
        {
            spiderSpawned[i] = false;
        }
        //var enemySpawnData = new EnemySpawnData();
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
                if (Vector3.Distance(playerLocation, rangedSpawns[i].position) > 50 && rangedSpawned[i])
                {
                    rangedSpawned[i] = false;
                    Destroy(rangedSpawns[i].GetChild(0).gameObject);
                }
                else if (Vector3.Distance(playerLocation, rangedSpawns[i].position) < 50 && !rangedSpawned[i])
                {
                    rangedSpawned[i] = true;
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
                if (Vector3.Distance(playerLocation, meleeSpawns[i].position) > 50 && meleeSpawned[i])
                {
                    meleeSpawned[i] = false;
                    Destroy(meleeSpawns[i].GetChild(0).gameObject);
                }
                else if (Vector3.Distance(playerLocation, meleeSpawns[i].position) < 50 && !meleeSpawned[i])
                {
                    meleeSpawned[i] = true;
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
            if (Vector3.Distance(playerLocation, spiderSpawns[i].position) > 50 && spiderSpawned[i])
            {
                spiderSpawned[i] = false;
                Destroy(spiderSpawns[i].GetChild(0).gameObject);
            }
            else if (Vector3.Distance(playerLocation, spiderSpawns[i].position) < 50 && !spiderSpawned[i])
            {
                spiderSpawned[i] = true;
                GameObject spawn = Instantiate(spider, spiderSpawns[i].position, spiderSpawns[i].rotation);
                spawn.transform.SetParent(spiderSpawns[i]);
            }
        }
        yield return new WaitForSeconds(Timer);
        StartCoroutine("CheckSpider");
    }
}
