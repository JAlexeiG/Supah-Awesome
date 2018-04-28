using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaCoil : Power {

    Transform player;
    Vector3 playerLocation;
    [SerializeField] float teslaStunLength;
    [SerializeField] SphereCollider range;
    float attackSpeed = 4f;
    float teslaDamage = 0f;
    LineRenderer line;
    Vector3[] linePositions;
    bool isActive;
    Renderer rend;
    static float explosionStrength = 357f;

    [SerializeField] float onTimer;
    [SerializeField] float offTimer;

    // Use this for initialization
    void Start () 
    {
        isPowered = true;
        rend = GetComponent<Renderer>();
        line = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("SwitchPower");
        teslaStunLength = 0.5f;
    }

    // Update is called once per frame
    void Update () 
    {
        playerLocation = player.position;
        Vector3 direction = (player.position - transform.position).normalized;
        //Debug.Log(Vector3.Distance(transform.position, player.position) + " " + range.radius);

        if (Vector3.Distance(transform.position, player.position) < range.radius && isActive && isPowered)
        {
            Attack();
            Debug.Log("hello");
        }
	}

    void Attack()
    {
        linePositions = new Vector3[2];
        linePositions[0] = gameObject.transform.position;
        linePositions[1] = playerLocation;
        HealthManager.instance.health -= teslaDamage;
        Chara chara = player.gameObject.GetComponent<Chara>();
        chara.callStun(teslaStunLength);
        line.SetPositions(linePositions);
        player.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionStrength, gameObject.transform.position, 100, 1);
    }
    /*
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && isActive && isPowered)
        {
            Debug.Log(col.name + " is near " + gameObject);
            {
                linePositions = new Vector3[2];
                linePositions[0] = gameObject.transform.position;
                linePositions[1] = playerLocation;
                HealthManager.instance.health -= teslaDamage;
                Chara chara = player.gameObject.GetComponent<Chara>();
                chara.callStun(teslaStunLength);
                line.SetPositions(linePositions);
            }
        }
    }
    */

    IEnumerator SwitchPower()
    {
        if (isActive && isPowered)
        {
            yield return new WaitForSeconds(offTimer);
            isActive = false;
            rend.material.color = Color.red;
            //Debug.Log("Tesla off");
            StartCoroutine("SwitchPower");
        }

        else if (!isActive && isPowered)
        {
            yield return new WaitForSeconds(onTimer);
            isActive = true;
            rend.material.color = Color.green;
            //Debug.Log("Tesla on");
            StartCoroutine("SwitchPower");
        }

        else
        {
            isActive = false;
            //Debug.Log("Tesla power turned off. No longer active.");
            yield return null;
        }
    }
}
