﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraAngle : MonoBehaviour {

    [SerializeField]
    bool Angled;
    [SerializeField]
    bool Fixed;


    GameObject MainCam;
    CameraController Cam;
    Vector3 location;

    // Use this for initialization
    void Start () {
        MainCam = GameObject.Find("Main Camera");
        Cam = MainCam.GetComponent<CameraController>();
        Cam.camNum = 1;
        location = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Angled)
            {
                Cam.camNum = 2;
            }
            else if (Fixed)
            {
                Cam.tempFixLocation = location;
                Cam.camNum = 3;
                Cam.exitingFixed = true;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Cam.camNum = 1;
        }
    }
}
