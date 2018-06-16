using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public GameObject TrackObject;
    public Vector3 Offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(TrackObject.transform.position) + Offset;
	}
}
