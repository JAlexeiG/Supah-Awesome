using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSwitch : MonoBehaviour {

    public GameObject[] pow;
    [SerializeField]
    private int switchTime = 5;
    int timer;
    private bool switchOn;
    bool activated = false;

    [SerializeField]
    Text timerText;
    // Use this for initialization
    void Start () 
    {
        switchOn = false;
        timer = switchTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        timerText.text = timer + " ";
        if (switchOn)
        {
            Debug.Log(timer);
            if (timer < 1)
            {
                activated = false;
                switchOn = false;
                timer = switchTime;
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
            if (!activated)
            {
                activated = true;
                switchOn = !switchOn;
                timer = switchTime;
                StartCoroutine("CountDown");
                foreach (GameObject i in pow)
                {
                    i.GetComponent<Power>().isPowered = !i.GetComponent<Power>().isPowered;
                    Debug.Log(i.gameObject.name + " is powered: " + i.GetComponent<Power>().isPowered);
                }
            }
            else if (activated)
            {
                timer = switchTime;
            }
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        timer--;
        if (timer > 0 && activated)
            StartCoroutine("CountDown");
    }
}
