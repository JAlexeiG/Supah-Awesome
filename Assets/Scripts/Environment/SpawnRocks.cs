using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocks : MonoBehaviour {

	public Transform[] SpawnPoints;
	public float spawnTime = 2.0f;


	public GameObject[] Rocks;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("SpawnRock", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public void SpawnRock()
	{
		int spawnIndex = Random.Range (0, SpawnPoints.Length);
		int rockIndex = Random.Range (0, Rocks.Length);
		Instantiate (Rocks[rockIndex], SpawnPoints [spawnIndex].position, SpawnPoints [spawnIndex].rotation);
	}
}
