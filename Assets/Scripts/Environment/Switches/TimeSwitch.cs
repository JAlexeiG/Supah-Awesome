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
                switchOn = false;
                timer = 0;
                foreach (GameObject i in pow)
                {
                    i.GetComponent<Power>().isPowered = !i.GetComponent<Power>().isPowered;
                    Debug.Log(i.gameObject.name + " is powered: " + i.GetComponent<Power>().isPowered);
                }
               
            }
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") //add && melee input
        {
            timer = 0;
            switchOn = !switchOn;
            foreach (GameObject i in pow)
            {
                i.GetComponent<Power>().isPowered = !i.GetComponent<Power>().isPowered;
                Debug.Log(i.gameObject.name + " is powered: " + i.GetComponent<Power>().isPowered);
            }
        }
    }       
}
