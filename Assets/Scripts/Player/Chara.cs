using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Chara : MonoBehaviour
{
    [SerializeField]
    Transform SpawnPoint;

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
    private bool gliderStarted;
    [SerializeField]
    private float minGliderSpeed;


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
    bool isPaused;

    [SerializeField]
    private Text bulletText;


    private bool isMele;
    [SerializeField]
    private float meleTime;
    private float meleTimer;
    [SerializeField]
    private float meleCoolDown;
    private float meleCoolDownTimer;
    [SerializeField]
    private GameObject meleBox;
    

    void Start()
    {
        isMele = false;
        isStunned = false;
        isPaused = false;
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

        doubleJump = false;
        gliderStarted = false;
    }

    void Update()
    {
        if (transform.position.y < -10 || transform.position.y > 100)
        {
            transform.position = SpawnPoint.position;
        }

        bulletText.text = string.Format("Bullets Loaded: {0}\nAmmo: {1}" , bulletLoaded, playerBullets);
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
    private void OnCollisionStay(Collision collision)
    {
        grounded = checkGrounded();
        
    }
    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
    
    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SteamManager.instance.isPaused = !SteamManager.instance.isPaused;
            isPaused = !isPaused;
        }

        if (!isStunned && !isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isMele = !isMele;
            }

            if (isMele)
            {
                if (Input.GetButtonDown("Fire1") & meleCoolDownTimer <= 0)
                {
                    gunPos = -Camera.main.transform.position.z;
                    meleTimer = meleTime;
                    //Mouse position (+20 because camera is -20) to find where to shoot something
                    mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, trans.position.z + gunPos);


                    Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

                    GameObject crosshair = Instantiate(crosshairPreFab, potato, Quaternion.Euler(0, 0, 0));

                    ///Updates for aiming
                    aimingOrigin.LookAt(crosshair.transform);
                    Destroy(crosshair, 0.5f);
                }
                if (meleTimer > 0)
                {
                    meleBox.SetActive(true);
                    meleTimer -= Time.deltaTime;
                    meleCoolDownTimer = meleCoolDown;
                }
                else
                {
                    meleBox.SetActive(false);
                    meleCoolDownTimer -= Time.deltaTime;
                }
            }

            if (!isMele)
            {
                meleBox.SetActive(false);
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
                //}

                //if (dashing)
                //{
                //    dashTimer += Time.deltaTime;


                //    if (dashPos.x < trans.position.x)
                //    {
                //        rb.velocity = new Vector3(-dashStrength, 0, 0);
                //    }
                //    else if (dashPos.x > trans.position.x)
                //    {
                //        rb.velocity = new Vector3(dashStrength, 0, 0);
                //    }
                //    //moveDirection = new Vector3(dashStrength, 0, 0); //Adds movement


                //    if (dashTimer >= dashLength)
                //    {
                //        dashing = false;
                //        rb.velocity = new Vector3(0, 0, 0);
                //    }
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

                //doubleJump = true; //Makes jumping available again
                canMove = true;


                //if (Input.GetButtonDown("Fire2"))
                //{
                //    mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
                //    dashPos = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

                //    dashing = true;
                //    dashTimer = 0;
                //    SteamManager.instance.steam -= 10; // Lowers steam by a number
                //}

                if (Input.GetButtonDown("Jump") && checkGrounded())
                {
                    rb.AddRelativeForce(0, jumpSpeed, 0, ForceMode.Impulse);
                    gliderStarted = false;
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
                if (!gliderStarted)
                {
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
                }
                else
                {
                    if (input > 0.1f && SteamManager.instance.steamUsable)
                    {
                        if (rb.velocity.x < speed * 1.5)
                        {
                            rb.velocity += new Vector3(airSpeed * 2, 0) * Time.deltaTime;
                            SteamManager.instance.steam--; //Lowers steam by 1 per frame
                        }
                        else
                        {
                            SteamManager.instance.steam--; //Lowers steam by 1 per frame
                        }
                    }
                    else if (input < -0.1f && SteamManager.instance.steamUsable)
                    {
                        if (rb.velocity.x > -speed * 1.5)
                        {
                            rb.velocity += new Vector3(-airSpeed * 2, 0) * Time.deltaTime;
                            SteamManager.instance.steam--; //Lowers steam by 1 per frame
                        }
                        else
                        {
                            SteamManager.instance.steam--; //Lowers steam by 1 per frame
                        }
                    }
                    else
                    {
                        rb.velocity -= new Vector3(rb.velocity.x * 0.7f, 0) * Time.deltaTime;
                    }
                }

                //if (doubleJump == true & SteamManager.instance.steamUsable == true)//Checks for double jump and jumps
                //{
                //    if (Input.GetButtonDown("Fire2"))
                //    {

                //        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);

                //        Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired
                //        Vector3 distance;
                //        if (!onWall)
                //        {
                //            distance = (potato - trans.position).normalized;
                //        }
                //        else
                //        {
                //            distance = (trans.position - potato).normalized;
                //        }

                //        Debug.Log(distance);

                //        SteamManager.instance.steam -= 25; // Lowers steam by a number
                //        doubleJump = false;//Turns it of


                //        if ((distance.x > 0 && rb.velocity.x > 0) || (distance.x < 0 && rb.velocity.x < 0))
                //        {
                //            rb.AddRelativeForce(distance * jumpSpeed, ForceMode.VelocityChange);
                //        }
                //        else
                //        {
                //            rb.velocity = Vector3.zero;
                //            rb.AddRelativeForce(distance * jumpSpeed, ForceMode.Impulse);
                //        }
                //    }
                //}

                if (Input.GetButton("Glider") && !grounded) // Button is Shift
                {
                    if (!gliderStarted)
                    {
                        if (trans.eulerAngles.z % 180 == 0)
                        {
                            if (rb.velocity.y * trans.up.y > 0)
                            {
                                rb.velocity = new Vector3(rb.velocity.x / 2,rb.velocity.y / 2);
                                gliderStarted = true;
                            }
                            else if (-rb.velocity.y > minGliderSpeed)
                            {
                                rb.velocity = new Vector3(rb.velocity.x / 2, rb.velocity.y / 2);
                                gliderStarted = true;
                            }
                        }
                    }
                    else
                    {
                        if (trans.eulerAngles.z % 180 == 0)
                        {
                            if (rb.velocity.y > minGliderSpeed || -rb.velocity.y < minGliderSpeed)
                            {
                                rb.velocity -= new Vector3(0, rb.velocity.y) * Time.deltaTime;
                            }
                        }
                    }
                    canMove = true;
                    gravity = OGravity / gliderStrength; // Lowers gravity
                    speed = OSpeed;
                }
                else if (Input.GetButtonUp("Glider"))
                {
                    gliderStarted = false;
                    gravity = OGravity;
                }
                else
                {
                    gliderStarted = false;
                    canMove = false;
                    gravity = OGravity;
                }
            }


        }
        if (isStunned)
        {
            gravity = OGravity;
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

    public struct XMLPlayer
    {
        public bool isMele;
        public float health;
        public float steam;
        public int loadedAmmo;
        public int totalAmmo;
        public Vector3 position;
        public Vector3 velocity;
    }

    public XMLPlayer GetXMLPlayer()
    {
        XMLPlayer playerXML = new XMLPlayer();
        playerXML.isMele = isMele;
        playerXML.health = HealthManager.instance.health;
        playerXML.steam = SteamManager.instance.steam;
        playerXML.loadedAmmo = bulletLoaded;
        playerXML.totalAmmo = playerBullets;
        playerXML.position = trans.position;
        playerXML.velocity = rb.velocity;

        return playerXML;
    }

    public void SaveXMLPlayer(XMLPlayer playerXML)
    {
        isMele = playerXML.isMele;
        HealthManager.instance.health = playerXML.health;
        SteamManager.instance.steam = playerXML.steam;
        bulletLoaded = playerXML.loadedAmmo;
        playerBullets = playerXML.totalAmmo;
        trans.position = playerXML.position;
        rb.velocity = playerXML.velocity;
    }
}
