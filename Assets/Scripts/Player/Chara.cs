using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara : MonoBehaviour
{

    public float speed;
    public float jumpSpeed;
    public float gravity;


    private float OGravity;
    private float OSpeed;
    private Vector3 moveDirection;

    private Rigidbody rb;

    public float boostStrength;
    public float gliderStrength;


    private bool grounded;

    //Left and right for booster via arrow keys
    //float up;
    //float right;
    
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
    float dashTimer;

    Transform trans;

    static bool onWall_;
    [SerializeField]
    private float rad_angle;
    [SerializeField]
    private float angle_;
    [SerializeField]
    private float rotationSpeed;

    public bool doubleJump;

    private Vector3 mouseMovement;
    private Vector3 dashAngle;

    void Start()
    {
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
    }

    void Update()
    {

        rb.AddRelativeForce(0, gravity, 0, ForceMode.Acceleration); //Adds gravity downwards towards the player's feet and only towards the player's feet


        //Updates to make sure everything is not over the cap
        if (playerBullets > bulletCap)
        {
            playerBullets = bulletCap;
        }



        //All the inputsfor boosting in air
        /*
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
                up = boostStrength;
            }
            else if (Input.GetAxis("Vertical") < -0.1)
            {
                up = -boostStrength;
            }
            else
            {
                up = 0;
            }
        */


        //A bunch of stuff to know where mouse is
        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletLoaded != 0)
            {
                //Mouse position (+20 because camera is -20) to find where to shoot something
                mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);

                mouseMovement = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

                GameObject crosshair = Instantiate(crosshairPreFab, mouseMovement, Quaternion.Euler(0, 0, 0));


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
        
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
        if (SteamManager.instance.steamUsable == true && Input.GetMouseButtonDown(1) && !dashing)
        {
            dashing = true;
            SteamManager.instance.steam -= 10;
            dashTimer = 0;
            //Mouse position (+20 because camera is -20) to find where to shoot something
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);

            dashAngle = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired
        }

        if (dashing)
        {
            Vector3 direction = trans.position - dashAngle;
            Vector3 normal = direction.normalized;
            if (grounded)
            {
                dashTimer += Time.deltaTime;


                if (normal.x < 0)
                {
                    rb.velocity = new Vector3(dashStrength, 0, 0);
                }
                else if (normal.x > 0)
                {
                    rb.velocity = new Vector3(-dashStrength, 0, 0);
                }
                //moveDirection = new Vector3(dashStrength, 0, 0); //Adds movement


                if (dashTimer >= dashLength)
                {
                    dashing = false;
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }
            else if (doubleJump)
            {
                rb.AddForce(normal * jumpSpeed);
                dashing = false;
                doubleJump = false;
            }
        }

        if (grounded && !dashing)
        {
            doubleJump = true; //Makes jumping available again



            if (Input.GetButtonDown("Jump"))
            {
                rb.AddRelativeForce(0, jumpSpeed, 0, ForceMode.VelocityChange);
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

                    //rb.AddRelativeForce(right, up, 0, ForceMode.VelocityChange);

                }
            }

            if (Input.GetButton("Glider") & SteamManager.instance.steamUsable == true) // Button is Shift
            {
                gravity = OGravity / gliderStrength; // Lowers gravity
                SteamManager.instance.steam--; //Loweres steam by one per frame
            }
            else
            {
                gravity = OGravity;
            }
            
        }



        float input = Input.GetAxis("Horizontal");

        if (input > 0)
        {
            trans.position += trans.right * speed * Time.deltaTime;
        }
        else if (input < 0)
        {
            trans.position += -trans.right * speed * Time.deltaTime;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            grounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            grounded = false;
        }
    }
}
