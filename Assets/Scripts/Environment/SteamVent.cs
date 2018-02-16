using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVent : MonoBehaviour {

	Chara player;
    [SerializeField]
    float strength;
    public bool isPowered;
    Collider m_collider;

    // Use this for initialization
    void Start () {
        m_collider = GetComponent<Collider>();
        m_collider.enabled = isPowered;
    }
	
	// Update is called once per frame
	void Update () {
        m_collider.enabled = isPowered;
    }
    void OnTriggerStay(Collider other) 
	{
        if (other.tag == "Player")
        {
            player = other.GetComponent<Chara>();
            Debug.Log(other.gameObject.name + " has stepped on " + gameObject.name);
            player.GetComponent<Rigidbody>().AddExplosionForce(strength, transform.position ,strength);
            //player.GetComponent<Rigidbody>().AddRelativeForce(transform.up * strength, ForceMode.Acceleration);
        }
	}
    public void PowerSwitch()
    {
        isPowered = !isPowered;
        Debug.Log(gameObject + " power has been switched to " + isPowered);

    }
}
