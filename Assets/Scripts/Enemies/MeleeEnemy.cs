using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour {

    Rigidbody rb;
    Transform player;
    NavMeshAgent agent;
    Vector3 playerLocation;

    int meleeEnemyDamage = 15;
    bool attackCooldown = false;
    bool moveCooldown = false;
    bool isDying = false;

    [SerializeField] float maxHealth = 2f;
    [SerializeField] float currentHealth;

    [SerializeField] float hitTimer;
    float timer;
    [SerializeField] bool isHit;



    //Animation
    Animator animator;
    [SerializeField]
    AnimationClip[] animationClips;


    // Use this for initialization
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.stoppingDistance = 2f;
        agent.speed = 3f;
        agent.acceleration = 100f;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDying)
        {
            animator.SetBool("isDying", true);
            animator.SetBool("isWalking", false);
        }
            
        if (!isHit)
        {
            playerLocation = player.position;
            if (Vector3.Distance(transform.position, player.position) < 7f && !moveCooldown && !isDying)
            {
                agent.SetDestination(playerLocation);
                if (Vector3.Distance(transform.position, player.position) < 2.3f && !attackCooldown)
                {
                    if (!isDying)
                        Attack();
                }
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 2;
                getHit(false);
            }
        }

        if (currentHealth < 1)
        {
            StartCoroutine("DelayedDeath");
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 2f);
    }

    void Attack()
    {
        if (!isDying)
        {
            StartCoroutine("MoveCooldown");
            StartCoroutine("AttackCooldown");
            attackCooldown = true;
            moveCooldown = true;
            //play attacking animation
            playerLocation = player.position;
            if (Vector3.Distance(transform.position, player.position) < 2.3f)
            {
                HealthManager.instance.health -= meleeEnemyDamage;
                Debug.Log("hit player");
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        Debug.Log("start attack cooldown");
        yield return new WaitForSeconds(4);
        Debug.Log("end cooldown");
        attackCooldown = false;
    }

    IEnumerator MoveCooldown()
    {
        Debug.Log("start move cooldown");
        yield return new WaitForSeconds(3);
        Debug.Log("end move cooldown");
        moveCooldown = false;
    }

    public void DoDamage()
    {
        currentHealth -= 1;
        Debug.Log("Melee Enemy has " + currentHealth + " health left.");
    }

    IEnumerator DelayedDeath()
    {
        isDying = true;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        //Drop Loot
    }

    public void getHit(bool hit)
    {
        if (hit)
        {
            agent.enabled = false;
            rb.isKinematic = false;
            timer = hitTimer;
            isHit = true;
        }
        else
        {
            agent.enabled = true;
            rb.isKinematic = true;
            isHit = false;
        }
    }
}
