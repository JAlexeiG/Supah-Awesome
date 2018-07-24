using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb : Power 
{
    [SerializeField]
    Material[] materials;
    Renderer rend;
	// Use this for initialization
	void Start () 
    {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPowered)
        {
            rend.material = materials[0];
        }
        else
        {
            rend.material = materials[1];
        }
	}
}
