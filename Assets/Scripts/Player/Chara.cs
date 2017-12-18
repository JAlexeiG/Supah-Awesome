using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    public float gravity;

    private float OGravity;
    private float OSpeed;
    public Vector3 moveDirection = Vector3.zero;
    

    public float boostStrength;
    public float gliderStrength;

    //Left and right for booster via arrow keys
    float left;
    float right;

    //Where the mouse is
    Ray ray;
    Ray playerRay;
    private Vector3 mousePos;

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
    float dashInput;
    float dashTimer;


    public bool doubleJump;
    void Start()
    {
        
        //// Checks to make sure nothing is 0 and pre-sets the original speed and gravity
        if (speed <= 0)
        {
            speed = 4.0f;
        }
        if (gravity <= 0)
        {
            gravity = 4.0f;
        }
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
        if(dashTimer <= 0)
        {
            dashTimer = 0.1f;
        }
        OGravity = gravity;
        OSpeed = speed;
        ////

        bullets = 10;
        playerBullets = 5;
        bulletCap = 10;
        bulletLoaded = 0;
    }

    void Update()
    {
        //Updates to make sure everything is not over the cap
        if (playerBullets > bulletCap)
        {
            playerBullets = bulletCap;
        }



        //All the inputsfor boosting in air
        if (Input.GetAxis("Horizontal") > 0.1)
        {
            right = boostStrength;
        }
        else if (Input.GetAxis("Horizontal") < -0.1)
        {
            right = -boostStrength;
        }
        else
        {
            right = 0;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            left = boostStrength;
        }
        else if (Input.GetAxis("Vertical") < -0.1)
        {
            left = -boostStrength;
        }
        else
        {
            left = 0;
        }


        //A bunch of stuff to know where mouse is
        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletLoaded != 0)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Creates a ray where the mouse clicks
                playerRay = Camera.main.ScreenPointToRay(gameObject.transform.position);//Creates a ray from where the character is

                //Mouse position (+20 because camera is -20) to find where to shoot something
                mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);


                Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

                GameObject crosshair = Instantiate(crosshairPreFab, potato, Quaternion.Euler(0, 0, 0));


                ///Updates for aiming
                aimingOrigin.LookAt(crosshair.transform);

                GameObject bullet = Instantiate(bulletPreFab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;

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

        CharacterController controller = GetComponent<CharacterController>();

        if (Input.GetButtonDown("Dash") && SteamManager.instance.steamUsable == true)
        {
            dashing = true;
            dashTimer = 0;
            dashInput = Input.GetAxis("Dash");
            SteamManager.instance.steam -= 10;
        }
        if(dashing)
        {
            dashTimer += Time.deltaTime;


            moveDirection = new Vector3(dashStrength * dashInput, 0, 0); //Adds movement

            if (dashTimer >= dashLength)
            {
                dashing = false;
            }
        }

        if (controller.isGrounded && !dashing)
        {
            doubleJump = true; //Makes jumping available again
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0); //Adds movement
            moveDirection = transform.TransformDirection(moveDirection); //Moves stuff
            moveDirection *= speed; // Applies an acceleration to the movement

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;//Jump
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
            if (doubleJump == true & SteamManager.instance.steamUsable == true)//Checks for double jump and jumps
            {
                if (Input.GetButtonDown("Jump"))
                {
                    SteamManager.instance.steam -= 25; // Lowers steam by a number
                    doubleJump = false;//Turns it off
                    moveDirection += new Vector3(right, left, 0);//Adds a force to the char based on buttons held down
                }
            }

            if (Input.GetButton("Glider") & SteamManager.instance.steamUsable == true) // Button is Shift
            {
                gravity = OGravity / gliderStrength; // Lowers gravity
                SteamManager.instance.steam--; //Loweres steam by one per frame
            }
            else
            {
                gravity = OGravity; //Reesets gravity
            }
        }

        moveDirection.y -= gravity * Time.deltaTime; // Adds velocity downwards over tiem
        controller.Move(moveDirection * Time.deltaTime); // Moves the character according to velocity   
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
}
