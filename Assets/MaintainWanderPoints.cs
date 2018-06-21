using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainWanderPoints : MonoBehaviour {

    public Vector3 startPos;

	private void Awake()
	{
        startPos = transform.position;
	}

	// Use this for initialization
	void Start () {
        StartCoroutine("CheckPositions");
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    IEnumerator CheckPositions()
    {
        if (transform.position != startPos)
            transform.position = startPos;
        yield return new WaitForSeconds(5);
        StartCoroutine("CheckPositions");
    }
}
