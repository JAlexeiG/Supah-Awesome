using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWater : Power {
    
	
	// Update is called once per frame
	void Update () {
		
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
