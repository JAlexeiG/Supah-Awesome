using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    float damage = 5;

    private SphereCollider range;
    private Transform player;
    private Vector3 playerLocation;
    private bool shooting = false;

    public GameObject projectile;
    public Transform projectileSpawn;

    // Use this for initialization
    void Start()
    {
        range = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //scaleDamage = maxDamage - minDamage;
    }

    void Update()
    {
        playerLocation = player.position;
        if (Vector3.Distance(transform.position, player.position) < 10)
        {
            FaceTarget();
            if (!shooting)
                Attack();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 30f);
    }

    void Attack()
    {
        Debug.Log("shoot");
        shooting = true;
        StartCoroutine("Cooldown");
        GameObject bullet = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
        //float fractionalDistance = (range.radius - Vector3.Distance(transform.position, player.position)) / range.radius; //if player is standing on top of enemy this = 1, if player is at max range from enemy this = 0
        //float damage = scaleDamage * fractionalDistance + minDamage; //if standing on enemy, 4 * 1 + 1 = 5 aka max damage. if at max range, 4 * 0 + 1 = 1 aka min damage
    }

    IEnumerator Cooldown()
    {
        Debug.Log("start cooldown");
        yield return new WaitForSeconds(3);
        Debug.Log("end cooldown");
        shooting = false;
    }
}
