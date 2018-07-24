using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBulbSequence : Power {

    [SerializeField]
    GameObject[] bulbs; //the sequence of bulbs you want to power, drag them in order

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public IEnumerator PowerSequence()
    {
        for (int i = 0; i < bulbs.Length; i++)
        {
            bulbs[i].GetComponent<Bulb>().PowerSwitch();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
