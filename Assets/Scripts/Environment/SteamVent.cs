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
        isPowered = false;
        m_collider = GetComponent<Collider>();
        m_collider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

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
    public void PowerOn()
    {
        isPowered = true;
        m_collider.enabled = true;
        Debug.Log(gameObject + "is on");

    }
    public void PowerOff()
    {
        isPowered = false;
        m_collider.enabled = false;
        Debug.Log(gameObject + "is off");
    }
}
