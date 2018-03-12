using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaCoil : MonoBehaviour {

    Transform player;
    Vector3 playerLocation;
    public float teslaStunLength = 0.5f;
    [SerializeField] SphereCollider range;
    float attackSpeed = 4f;
    float teslaDamage = 10f;

    // Use this for initialization
    void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update () 
    {
        playerLocation = player.position;
        Vector3 direction = (player.position - transform.position).normalized;

        if (Vector3.Distance(transform.position, player.position) < range.radius)
        {
            //attack
        }
	}

    /*
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log(col.name + " is near " + gameObject);
            {
                HealthManager.instance.health -= teslaDamage;
                Chara chara = player.gameObject.GetComponent<Chara>();
                chara.callStun(teslaStunLength);
            }
        }
    }
    */
}
