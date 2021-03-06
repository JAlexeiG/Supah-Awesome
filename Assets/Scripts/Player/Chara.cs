﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara : MonoBehaviour
{

    public float gunPos;

    [SerializeField]
    private float feetDistance;

    public float speed;
    public float airSpeed;
    public float jumpSpeed;
    public float gravity;


    private float OGravity;
    private float OSpeed;
    private Vector3 moveDirection;

    private Rigidbody rb;

    public float boostStrength;
    public float gliderStrength;

    [SerializeField]
    private bool grounded;

    //Left and right for booster via arrow keys
    float up;
    float right;
    
    private Vector3 mousePos;
    private Vector3 dashPos;

    //What the mouse clicked
    private RaycastHit hit;

    [SerializeField]
    private int bullets;
    [SerializeField]
    private int playerBullets;
    [SerializeField]
    private int bulletCap;
    [SerializeField]
    private int bulletLoaded;

    [SerializeField]
    Transform aimingOrigin;
    [SerializeField]
    Transform bulletSpawnPoint;
    [SerializeField]
    GameObject bulletPreFab;
    [SerializeField]
    GameObject crosshairPreFab;


    public bool dashing;
    public float dashLength;
    public float dashStrength;
    float dashTimer;

    [SerializeField]
    private bool canMove;

    Transform trans;

    static bool onWall_;
    [SerializeField]
    private float rad_angle;
    [SerializeField]
    private float angle_;
    [SerializeField]
    private float rotationSpeed;

    public bool doubleJump;
    bool isStunned;

    [SerializeField]
    private Text bulletText;

    void Start()
    {
        isStunned = false;
        feetDistance = 1.2f;
        trans = GetComponent<Transform>();

        rb = GetComponent<Rigidbody>();

        if (!rb)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        //// Checks to make sure nothing is 0 and pre-sets the original speed and gravity
        if (speed <= 0)
        {
            speed = 4.0f;
        }
        gravity = -9.81f;
        if (jumpSpeed <= 0)
        {
            jumpSpeed = 5.0f;
        }
        if (boostStrength <= 0)
        {
            boostStrength = 6;
        }
        if (gliderStrength <= 0)
        {
            gliderStrength = 6;
        }
        if (dashStrength <= 0)
        {
            dashStrength = 10;
        }
        if (dashTimer <= 0)
        {
            dashTimer = 0.1f;
        }
        OGravity = gravity;
        OSpeed = speed;
        ////

        playerBullets = 5;
        bulletCap = 10;
        bulletLoaded = 0;

        ////

        onWall = false;
        grounded = false;
        canMove = true;
    }

    void Update()
    {
        bulletText.text = string.Format("Bullets Loaded: {0}\nAmmo: {1}" , bulletLoaded, playerBullets);

        Debug.Log(isStunned);
        PlayerInput();
        

        rb.AddRelativeForce(0, gravity, 0, ForceMode.Acceleration); //Adds gravity downwards towards the player's feet and only towards the player's feet


        //Updates to make sure everything is not over the cap
        if (playerBullets > bulletCap)
        {
            playerBullets = bulletCap;
        }
    }


    public void fire()
    {
        GameObject bullet = Instantiate(bulletPreFab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        bulletLoaded--;

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;

        Destroy(bullet, 3.0f);
    }

    public void addBullets(int x)
    {
        playerBullets += x;
    }

    public bool onWall
    {
        get { return onWall_; }
        set { onWall_ = value; }
    }
    public float angle
    {
        get { return angle_; }
        set { angle_ = value; }
    }

    bool checkGrounded()
    {
        Ray ray = new Ray(trans.position, -trans.up);
        RaycastHit hit;


        return Physics.Raycast(ray, out hit, feetDistance);


    }
    private void OnCollisionEnter(Collision collision)
    {
        grounded = checkGrounded();
    }

    void PlayerInput()
    {
        if (!isStunned)
        {
            //A bunch of stuff to know where mouse is
            if (Input.GetButtonDown("Fire1"))
            {
                gunPos = -Camera.main.transform.position.z;
                //// CHANGE THE SHOOTING THING TO BE NON-RELYANT ON THE CROSSHAIR
                if (bulletLoaded != 0)
                {
                    //Mouse position (+20 because camera is -20) to find where to shoot something
                    mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, trans.position.z + gunPos);


                    Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

                    GameObject crosshair = Instantiate(crosshairPreFab, potato, Quaternion.Euler(0, 0, 0));

                    ///Updates for aiming
                    aimingOrigin.LookAt(crosshair.transform);

                    GameObject bullet = Instantiate(bulletPreFab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

                    Destroy(bullet, 3.0f);
                    Destroy(crosshair, 0.5f);

                    bulletLoaded--;
                }
                else
                {
                    Debug.Log("You have no bullets loaded, re-loading");
                    if (playerBullets <= 0)
                    {
                        Debug.Log("You have no bullets left");
                    }
                    else
                    {
                        if (playerBullets < 5)
                        {
                            bulletLoaded = playerBullets;
                            playerBullets = 0;
                        }
                        else
                        {
                            playerBullets -= 5;
                            bulletLoaded += 5;
                        }
                    }
                }

            }


            if (dashing)
            {
                dashTimer += Time.deltaTime;


                if (dashPos.x < trans.position.x)
                {
                    rb.velocity = new Vector3(-dashStrength, 0, 0);
                }
                else if (dashPos.x > trans.position.x)
                {
                    rb.velocity = new Vector3(dashStrength, 0, 0);
                }
                //moveDirection = new Vector3(dashStrength, 0, 0); //Adds movement


                if (dashTimer >= dashLength)
                {
                    dashing = false;
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }

            if (grounded && !dashing)
            {

                float input = Input.GetAxis("Horizontal");

                if (input > 0.1f)
                {
                    rb.velocity = new Vector3(speed, rb.velocity.y);
                }
                else if (input < -0.1f)
                {
                    rb.velocity = new Vector3(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector3(0, rb.velocity.y);
                }

                doubleJump = true; //Makes jumping available again
                canMove = true;


                if (Input.GetButtonDown("Fire2"))
                {
                    mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
                    dashPos = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

                    dashing = true;
                    dashTimer = 0;
                    SteamManager.instance.steam -= 10; // Lowers steam by a number
                }

                if (Input.GetButtonDown("Jump") && checkGrounded())
                {
                    rb.AddRelativeForce(0, jumpSpeed, 0, ForceMode.Impulse);
                    grounded = false;
                }

                if (Input.GetButton("Glider") & SteamManager.instance.steamUsable == true) // Button is Shift
                {
                    speed = OSpeed * 3f; // Increases the max speed
                    SteamManager.instance.steam--; //Lowers steam by 1 per frame
                }
                else
                {
                    speed = OSpeed;
                }
            }
            else if (!dashing)
            {

                float input = Input.GetAxis("Horizontal");
                if (input > 0.1f)
                {
                    if (rb.velocity.x < speed)
                    {
                        rb.velocity += new Vector3(airSpeed, 0) * Time.deltaTime;
                    }
                }
                else if (input < -0.1f)
                {
                    if (rb.velocity.x > -speed)
                    {
                        rb.velocity += new Vector3(-airSpeed, 0) * Time.deltaTime;
                    }
                }

                if (doubleJump == true & SteamManager.instance.steamUsable == true)//Checks for double jump and jumps
                {
                    if (Input.GetButtonDown("Fire2"))
                    {

                        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);

                        Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired
                        Vector3 distance;
                        if (!onWall)
                        {
                            distance = (potato - trans.position).normalized;
                        }
                        else
                        {
                            distance = (trans.position - potato).normalized;
                        }

                        Debug.Log(distance);

                        SteamManager.instance.steam -= 25; // Lowers steam by a number
                        doubleJump = false;//Turns it of


                        if ((distance.x > 0 && rb.velocity.x > 0) || (distance.x < 0 && rb.velocity.x < 0))
                        {
                            rb.AddRelativeForce(distance * jumpSpeed, ForceMode.VelocityChange);
                        }
                        else
                        {
                            rb.velocity = Vector3.zero;
                            rb.AddRelativeForce(distance * jumpSpeed, ForceMode.Impulse);
                        }
                    }
                }

                if (Input.GetButtonDown("Glider"))
                {
                    rb.velocity = new Vector3(rb.velocity.x / 1.5f, rb.velocity.y / 1.5f);
                }
                if (Input.GetButton("Glider") & SteamManager.instance.steamUsable == true) // Button is Shift
                {
                    canMove = true;
                    gravity = OGravity / gliderStrength; // Lowers gravity
                    SteamManager.instance.steam--; //Loweres steam by one per frame
                    speed = OSpeed;
                }
                else
                {
                    canMove = false;
                    gravity = OGravity;
                }
            }


            float diff = Mathf.Abs(trans.eulerAngles.z - angle);
            if (diff > 5f)
            {
                if (trans.eulerAngles.z < angle)
                {
                    trans.eulerAngles += Vector3.Lerp(trans.eulerAngles, new Vector3(0, 0, angle), 5f) * Time.deltaTime * 5;
                }

                else if (trans.eulerAngles.z > angle)
                {
                    trans.eulerAngles -= Vector3.Lerp(new Vector3(0, 0, angle), trans.eulerAngles, 5f) * Time.deltaTime * 5;
                }
            }
            else
            {
                trans.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    public void callStun(float duration)
    {
        isStunned = true;
        StartCoroutine("stunTimer", duration);
    }

    IEnumerator stunTimer(float duration)
    {
        rb.velocity = Vector3.zero;
        gravity = OGravity;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }
}
