using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public GameObject[] pow;

	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "box")
        {
            foreach(GameObject i in pow)
            {
                i.GetComponent<Power>().PowerSwitch();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" || col.tag == "box")
        {
            foreach(GameObject i in pow)
            {
                i.GetComponent<Power>().PowerSwitch();
            }
        }
    }
}
