using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVentIndictator : Power {

    [SerializeField]
    public GameObject Particles;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(isPowered)
        {
            Particles.SetActive(true);
        }
        else
        {
            Particles.SetActive(false);
        }

	}
}
