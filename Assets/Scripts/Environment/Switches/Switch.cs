using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public GameObject[] pow;

    [SerializeField]
    Material[] materials;

    Renderer rend;
    bool switchOn;
    

	private void Start()
	{
        rend = GetComponent<Renderer>();
	}

	private void Update()
    {
        if (switchOn)
            rend.material = materials[0];
        else
            rend.material = materials[1];
    }

	void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player" || col.tag == "box")
        {
            switchOn = true;
            foreach (GameObject i in pow)
            {
                i.GetComponent<Power>().PowerSwitch(true);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" || col.tag == "box")
        {
            switchOn = false;
            foreach(GameObject i in pow)
            {
                i.GetComponent<Power>().PowerSwitch(false);
            }
        }
    }
}
