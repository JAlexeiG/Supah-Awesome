using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    
    public GameObject steamvent;
    public GameObject platform;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "box")
        {
            {
                Debug.Log(col.name + " is standing on " + gameObject);
                steamvent.GetComponent<SteamVent>().PowerOn();
                Debug.Log(platform + " is on");
                platform.GetComponent<MovingPlatform>().enabled = true;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" || col.tag == "box")
        {
            {
                Debug.Log(col.name + " is off the " + gameObject);
                steamvent.GetComponent<SteamVent>().PowerOff();
                Debug.Log(platform + " is off");
                platform.GetComponent<MovingPlatform>().enabled = false;

            }
        }
    }
}
