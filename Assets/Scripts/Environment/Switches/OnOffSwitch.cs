using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSwitch : Power {

    public GameObject[] pow;
    private bool switchOn;
    // Use this for initialization
    void Start()
    {
        switchOn = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") //add && melee input
        {
            foreach (GameObject i in pow)
            {
                i.GetComponent<Power>().PowerSwitch();
            }
        }
    }
}
