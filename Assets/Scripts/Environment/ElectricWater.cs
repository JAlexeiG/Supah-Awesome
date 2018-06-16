using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWater : Power 
{
    Renderer rend;

	private void Start()
	{
        rend = GetComponent<Renderer>();
	}

	private void Update()
	{
        if (isPowered)
        {
            rend.material.color = Color.cyan;
        }
        else rend.material.color = Color.blue;
	}

	void OnTriggerStay(Collider col)
    {
        if (col.name == "Player")
        {
            Debug.Log(col.name + " is on " + gameObject);
            if (isPowered)
            {
                HealthManager.instance.health -= 0.3f;
            }
        }
    }
}
