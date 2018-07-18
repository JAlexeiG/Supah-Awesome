using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxel : MonoBehaviour {

    [SerializeField] float rotationStrength;
    [SerializeField] MovingPlatform parentPlatform;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (parentPlatform.isPowered)
            transform.Rotate(0, 0, Time.deltaTime * rotationStrength);
	}
}
