using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSwitch : XMLSwitch {

    [SerializeField]
    Material[] materials;
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
            rend.material = materials[0];
        else rend.material = materials[1];

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
