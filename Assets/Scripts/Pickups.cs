using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour {

    //Items:
    /*
     1 - 3: Health
     4 - 5: Ammo
     6 - 10: Actual Items.
      
     */

    HealthManager healthMGR;
    Chara player;

    [SerializeField]
    private int item;

    private void Start()
    {
        healthMGR = FindObjectOfType<HealthManager>();
        player = FindObjectOfType<Chara>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (item)
            {
                case 1:
                    if (healthMGR.health < healthMGR.maxHealth)
                    {
                        healthMGR.health += 10;
                    }
                    break;
                case 2:
                    if (healthMGR.health < healthMGR.maxHealth)
                    {
                        healthMGR.health += 15;
                    }
                    break;
                case 3:
                    if (healthMGR.health < healthMGR.maxHealth)
                    {
                        healthMGR.health += 25;
                    }
                    break;
                case 4:
                    player.addBullets(1);
                    break;
                case 5:
                    player.addBullets(5);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
