using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaCoil : MonoBehaviour {

    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update () {
		
	}
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log(col.name + " is near " + gameObject);
            {
                HealthManager.instance.health -= 0.3f;
                StartCoroutine("Stun");
            }
        }
    }
    IEnumerator Stun()
    {
        player.GetComponent<Chara>().enabled = false;
        Debug.Log("start stun");
        yield return new WaitForSeconds(1);
        player.GetComponent<Chara>().enabled = true;
        Debug.Log("end stun");
    }
}
