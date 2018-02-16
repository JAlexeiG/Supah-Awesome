using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPlayer : MonoBehaviour {
    public Transform parent;
	void OnTriggerEnter(Collider col)
	{
        if (col.tag == "Player")
        {
            Debug.Log(col.name + " is on " + gameObject);
            col.transform.parent = parent;
        }
	}
	void OnTriggerExit(Collider col)
	{
        if (col.tag == "Player")
        {
            col.transform.parent = null;
        }
	}
}
