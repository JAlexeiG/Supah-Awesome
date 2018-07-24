using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSwitch : XMLSwitch {
    
    Renderer rend;
    [SerializeField]PowerBulbSequence pbs;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (powOn)
            rend.material.color = Color.green;
        else rend.material.color = Color.red;

        foreach (Power i in pow)
        {
            i.PowerSwitch(powOn);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            powOn = !powOn;
            if (pbs != null)
            {
                pbs.StartCoroutine("PowerSequence");
            }
            else
            {
                Debug.Log("No PBS system attatched");
            }
            
        }
    }


}
