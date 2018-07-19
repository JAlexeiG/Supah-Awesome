using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaCoil : Power {

    Transform player;
    Vector3 playerLocation;
    [SerializeField] float teslaStunLength;
    [SerializeField] SphereCollider range;
    float attackSpeed = 4f;
    float teslaDamage = 5f;
    LineRenderer line;
    Vector3[] linePositions;
    bool isActive;
    Renderer rend;
    [SerializeField]
    private float explosionStrength;

    [SerializeField]
    bool onCooldown;

    [SerializeField]
    GameObject baseObject;

    [SerializeField]
    Material[] baseMaterials;

    [SerializeField] float onTimer;
    [SerializeField] float offTimer;
    

    public Transform raycastTransform;

    // Use this for initialization
    void Start ()
    {
       explosionStrength = 357f;
        isPowered = true;
        rend = GetComponent<Renderer>();
        line = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("SwitchPower");
        teslaStunLength = 0.5f;
        onCooldown = false;
    }

    // Update is called once per frame
    void Update () 
    {
        playerLocation = player.position;
        Vector3 direction = (player.position - transform.position).normalized;
        //Debug.Log(Vector3.Distance(transform.position, player.position) + " " + range.radius);

        RaycastHit hit;

        if (Vector3.Distance(transform.position, player.position) < range.radius && isActive && isPowered)
        {
            if (Physics.Raycast(raycastTransform.position, direction, ~(1 << 12)))
            {
                if (!onCooldown)
                {
                    Attack();
                }
            }
        }
	}

    void Attack()
    {
        onCooldown = true;
        linePositions = new Vector3[2];
        linePositions[0] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
        linePositions[1] = playerLocation;
        HealthManager.instance.health -= teslaDamage;
        Chara chara = player.gameObject.GetComponent<Chara>();
        chara.callStun(teslaStunLength);
        line.SetPositions(linePositions);
        player.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionStrength, gameObject.transform.position, 100, 1);
        StartCoroutine("CooldownTimer");
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

    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(2);
        onCooldown = false;
    }


    IEnumerator SwitchPower()
    {
        if (isActive && isPowered)
        {
            yield return new WaitForSeconds(offTimer);
            isActive = false;
            MeshRenderer meshRend = baseObject.GetComponent<MeshRenderer>();
            meshRend.material = baseMaterials[0];
            StartCoroutine("SwitchPower");
        }

        else if (!isActive && isPowered)
        {
            yield return new WaitForSeconds(onTimer);
            isActive = true;
            MeshRenderer meshRend = baseObject.GetComponent<MeshRenderer>();
            meshRend.material = baseMaterials[1];
            StartCoroutine("SwitchPower");
        }

        else
        {
            isActive = false;
            MeshRenderer meshRend = baseObject.GetComponent<MeshRenderer>();
            meshRend.material = baseMaterials[0];
            yield return null;
        }
    }
}
