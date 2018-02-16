using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    
    public GameObject steamvent;

    public GameObject platform;
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "box")
        {
            {
                Debug.Log(col.name + " is standing on " + gameObject);
                if (steamvent != null)
                {
                    steamvent.GetComponent<SteamVent>().PowerSwitch();
                }


                Debug.Log(platform + " is stepped on");
                if (platform != null)
                {
                    platform.GetComponent<MovingPlatform>().PowerSwitch();
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" || col.tag == "box")
        {
            {
                Debug.Log(col.name + " is standing on " + gameObject);
                if (steamvent != null)
                {
                    steamvent.GetComponent<SteamVent>().PowerSwitch();
                }


                Debug.Log(platform + " is stepped on");
                if (platform != null)
                {
                    platform.GetComponent<MovingPlatform>().PowerSwitch();
                }
            }
        }
    }
}
