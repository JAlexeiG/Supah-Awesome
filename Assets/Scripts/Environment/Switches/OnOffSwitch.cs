using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSwitch : Power {

    public GameObject[] pow;
    private bool switchOn;
    Renderer rend;

    // Use this for initialization
    void Start()
    {
        switchOn = false;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (switchOn)
            rend.material.color = Color.green;
        else rend.material.color = Color.red;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") //add && melee input
        {
            switchOn = !switchOn;
            foreach (GameObject i in pow)
            {
                i.GetComponent<Power>().PowerSwitch();
            }
        }
    }
}
