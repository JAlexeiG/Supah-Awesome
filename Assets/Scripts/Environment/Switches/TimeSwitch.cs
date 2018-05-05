using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwitch : Power {

    public GameObject[] pow;
    [SerializeField]
    private float switchTime = 5f;
    private float timer = 0;
    private bool switchOn;
    // Use this for initialization
    void Start () {
        switchOn = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (switchOn)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer > switchTime)
            {
                foreach (GameObject i in pow)
                {
                    i.GetComponent<Power>().isPowered = false;
                }
               
            }
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") //add && melee input
        {
            timer = 0;
            switchOn = true;
            foreach (GameObject i in pow)
            {
                i.GetComponent<Power>().isPowered = true;
            }
        }
    }
}
