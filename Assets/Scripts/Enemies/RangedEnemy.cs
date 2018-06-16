using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    float damage = 5;

    bool isDying = false;
    private SphereCollider range;
    private Transform player;
    private Vector3 playerLocation;
    private bool shooting = false;

    public GameObject projectile;
    public Transform projectileSpawn;

    [SerializeField] float maxHealth = 1f;
    [SerializeField] float currentHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        range = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //scaleDamage = maxDamage - minDamage;
    }

    void Update()
    {
        playerLocation = player.position; //players position
        if (Vector3.Distance(transform.position, player.position) < (range.radius * 1.2f))
        {
            FaceTarget();
        }
        if (Vector3.Distance(transform.position, player.position) < range.radius) 
        {
            if (!shooting)
                if (!isDying)
                    Attack();
        }

        if (currentHealth <= 0)
        {
            StartCoroutine("DelayedDeath");
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

    public void DoDamage()
    {
        currentHealth -= 1;
        Debug.Log("health now " + currentHealth);
    }

    IEnumerator DelayedDeath()
    {
        isDying = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        //Drop Loot
    }
}
