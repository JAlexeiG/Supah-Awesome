using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Chara : MonoBehaviour
{

    /// <summary>
    /// Controlls the entirety of the character, character physics, XML for the character, character spawn and character physics
    /// </summary>


    [SerializeField]
    Transform SpawnPoint; // Spawn of the character

    public ParticleSystem ammoPE; // ammo particle system
    public ParticleSystem healthPE; // health particle system
    
    public float gunPos; // controlls the z position of where the gun will shoot

    [SerializeField]
    private float feetDistance; //No longer used// holds distance for raycast of distance for character to be considered grounded
    

    public float speed; // shows and holds current max speed
    public float airSpeed; // shows and holds max air speed
    public float jumpSpeed; // shows and holds initial jump velocity
    public float gravity; //shows and holds current gravity


    private float OGravity; //shows and holds base gravity
    private float OSpeed; //shows and holds base speed
    private Vector3 moveDirection; // holds movement direction vector

    private Rigidbody rb; // character rigidbody

    public float boostStrength; //No longer used//  holds double jump strength
    public float gliderStrength; // strength of glider's anti-grav

    [SerializeField]
    private bool grounded; //For if player hitbox collides with anything

    [SerializeField]
    private bool grounded2; //For if player triggerbox collides with floor / box


    //Left and right for booster via arrow keys
    float up;
    float right;
    
    // position of mouse for functions requiering mouse position
    private Vector3 mousePos;
    private Vector3 dashPos; //No longer used//
    
    
    // Current bullets in pack - max bullets allowed in pack - loaded bullets
    [SerializeField]
    private int playerBullets;
    [SerializeField]
    private int bulletCap;
    [SerializeField]
    private int bulletLoaded;



    [SerializeField]
    Transform meleAim; //Where the transfrom for the mele will aim
    [SerializeField]
    Transform aimingOrigin; // Transfrom for the mele
    [SerializeField]
    Transform bulletSpawnPoint; //Where player bullets will spawn
    [SerializeField]
    GameObject bulletPreFab; //Prefab of the bullet
    [SerializeField]
    GameObject crosshairPreFab; //Prefab of crosshair for shooting

    //No longer used//
    public bool dashing; // Whether or not player is dashing
    public float dashLength; //How long the dash will be for
    public float dashStrength; //Strength of velocity for dash
    float dashTimer; //Countdown if currently dashing


    [SerializeField]
    private bool gliderStarted; //Whether player has just started gliding
    [SerializeField]
    private float minGliderSpeed; // The slowest possible speed the glider can be before no longer lowering speed


    //[SerializeField]
    // private bool canMove;

    Transform trans; //Transform of player

    static bool onWall_; //Whether player is on a magnetic wall
    [SerializeField]
    private float rad_angle; //Rad angle of player
    [SerializeField]
    private float angle_; //Deg angle of player
    [SerializeField]
    private float rotationSpeed; //Speed player rotates when rotating

    public bool doubleJump;  //No longer used// Whether double jump has been used
    bool isStunned; // Whether player is stunned
    bool isPaused; // Whether game is paused

    [SerializeField]
    private Text bulletText; // Current text for information on bullets
    
    [SerializeField]
    private float shootCoolDown; // Cooldown for shooting attacks
    private float shootCooldownTimer; // Timer for shootCoolDown

    [SerializeField]
    private bool isMele; // Whether player is in mele form
    [SerializeField]
    private float meleTime; // Time mele hitbox exists
    private float meleTimer; // Timer for meleTime
    [SerializeField]
    private float meleCoolDown; // Cooldown for mele attacks
    private float meleCoolDownTimer; // Timer for meleCoolDown
    [SerializeField]
    private GameObject meleBox; //Hitbox for mele attack

    // KEEP ALL ART HERE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ KEEP ALL ART HERE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ KEEP ALL ART HERE //

    [SerializeField]
    private Transform Arthur;

    [SerializeField]
    private Animator anim;


    [SerializeField]
    private GameObject glider; //Glider 

    // KEEP ALL ART HERE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ KEEP ALL ART HERE ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ KEEP ALL ART HERE //


    void Start()
    {

        //Sets everything to false initially 
        isMele = false;
        isStunned = false;
        isPaused = false;

        onWall = false;
        grounded = false;
        grounded2 = false;
        //canMove = true; //No longer used//

        doubleJump = false;
        gliderStarted = false;

        feetDistance = 1.2f; //No longer used//

        // Sets player transform and rigidbody
        trans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        if (!rb)
        { 
            //If someone forgets to add a rigidbody, adds and sets rigidbody
            gameObject.AddComponent<Rigidbody>();
            rb = GetComponent<Rigidbody>();
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

        //Pre-sets gravity
        OGravity = gravity;
        OSpeed = speed;
        ////

        //Pre-sets bullets
        playerBullets = 5;
        bulletCap = 10;
        bulletLoaded = 0;
        
    }

    void Update()
    {
        /// DROP DEATH
        if (transform.position.y < -30 || transform.position.y > 150)
        {
            HealthManager.instance.respawn(); //Auto-respawns character if hits a certain height
        }

        bulletText.text = string.Format("Bullets Loaded: {0}\nAmmo: {1}" , bulletLoaded, playerBullets); //Sets bullet text

        rb.AddRelativeForce(0, gravity * 2, 0, ForceMode.Acceleration); //Adds gravity downwards towards the player's feet and only towards the player's feet
        PlayerInput(); // Activates inputs for player
        



        //Updates to make sure everything is not over the cap
        if (playerBullets > bulletCap)
        {
            playerBullets = bulletCap;
        }

    }


    public void fire(GameObject crosshair)
    {
        // Spawns bullet, decreases current bullets and makes bullets automatically be destroyed
        
        GameObject bullet = Instantiate(bulletPreFab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

        Destroy(bullet, 3.0f);

        Destroy(crosshair, 0.5f); //Destroys the crosshair afterwards

        bulletLoaded--;
    }

    public void addBullets(int x)
    {
        playerBullets += x; // Increases current bullets
    }

    public bool onWall
    {
        //Changes and returns if player is on a wall
        get { return onWall_; }
        set { onWall_ = value; }
    }
    public float angle
    {
        //Changes and returns player angle
        get { return angle_; }
        set { angle_ = value; }
    }

    public void setGrounded(bool set)
    {
        //Sets player's grounded if on trigger
        grounded2 = set;
    }

    //Sets player's grounded if collides with anything
    private void OnCollisionStay(Collision collision)
    {
            grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
            grounded = false;
    }
    
    //All information for player inputs
    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pauses game
            SteamManager.instance.isPaused = !SteamManager.instance.isPaused;
            isPaused = !isPaused;
        }

        if (!isStunned) //If the player is not stunned and game is not paused
        {
            if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Q) && !(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Wep Transition Blend"))
            {
                //Changes between mele / ranged
                isMele = !isMele;
                anim.SetTrigger("Weapon Transition");
                anim.SetBool("Gun Not Sword",!isMele);
                anim.SetFloat("Weapon Transition Mult", -anim.GetFloat("Weapon Transition Mult"));
            }

            if (isMele) //When player is mele
            {
                if (Input.GetButtonDown("Fire1") && meleCoolDownTimer <= 0) //When player meles and is not on cooldown
                {
                    gunPos = -Camera.main.transform.position.z; //Sets the current transfrom of the camera

                    meleTimer = meleTime; //Starts up mele cooldown

                    //Mouse position (+20 because camera is -20) to find where to shoot something
                    mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, trans.position.z + gunPos);


                    Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

                    GameObject crosshair = Instantiate(crosshairPreFab, potato, Quaternion.Euler(0, 0, 0)); //Spawns crosshair at where you just fired

                    ///Updates for aiming
                    meleAim.LookAt(crosshair.transform); //Makes mele look at the crosshair
                    Destroy(crosshair, 0.5f); //Destroyes the crosshair
                }

                if (meleTimer > 0)
                {
                    //While the mele timer is up, sets mele hitbox, counts down timer and sets cooldown
                    meleBox.SetActive(true); // 
                    meleTimer -= Time.deltaTime;
                    meleCoolDownTimer = meleCoolDown;
                }
                else
                {
                    // Starts cooldown and turns off mele hitbox
                    meleBox.SetActive(false);
                    meleCoolDownTimer -= Time.deltaTime;
                }
            }

            if (!isMele)
            {
                //When player is ranged

                meleBox.SetActive(false); //Just in case mele hitbox existed

                //A bunch of stuff to know where mouse is
                if (Input.GetButtonDown("Fire1") && shootCooldownTimer <= 0)
                {
                    shootCooldownTimer = shootCoolDown;
                    gunPos = -Camera.main.transform.position.z; //Position of camera

                    //// CHANGE THE SHOOTING THING TO BE NON-RELYANT ON THE CROSSHAIR

                    if (bulletLoaded > 0) //If player has bullets
                    {


                        //Mouse position (+20 because camera is -20) to find where to shoot something
                        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, trans.position.z + gunPos);


                        Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

                        GameObject crosshair = Instantiate(crosshairPreFab, potato, Quaternion.Euler(0, 0, 0)); //Spawns crosshair

                        ///Updates for aiming


                        //aimingOrigin.LookAt(crosshair.transform); //Makes aim look at crosshair

                        //POTATOES// 

                        //Updates to prevent player from shooting into an object (that is not an enemy)

                        int layercast = ~(1 << 11); //Sets a layermask

                        RaycastHit hit; //Info of object hit

                        Ray newRay = new Ray(aimingOrigin.position, aimingOrigin.forward); //Ray for raycast
                        if (Physics.Raycast(newRay, out hit, 2.2f, layercast))
                        {
                            Debug.Log("Object in the way of shooting: " + hit.transform.name); //Tells you if something is in the way
                        }
                        else
                        {
                            fire(crosshair); //If nothing is in the way, shoot
                        }
                    }
                    else
                    {
                        //If player has no bullets

                        Debug.Log("You have no bullets loaded, re-loading"); //Tries to reload

                        if (playerBullets <= 0)
                        {
                            Debug.Log("You have no bullets left"); //If no bullets available, tells the player he has no bullets left
                        }
                        else
                        {
                            if (playerBullets < 5)
                            {
                                //If the player cannot reload all bullets, only reloads current bullets
                                bulletLoaded = playerBullets;
                                playerBullets = 0;
                            }
                            else
                            {
                                // Reloads 5 bullets
                                playerBullets -= 5;
                                bulletLoaded += 5;
                            }
                        }
                    }

                }
                else
                {
                    shootCooldownTimer -= Time.deltaTime;
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

            if (grounded && grounded2 && !dashing)
            {

                float input = Input.GetAxis("Horizontal");

                if (input > 0.1f)
                {
                    rb.velocity = new Vector3(speed, rb.velocity.y);
                    anim.SetBool("Running",true);
                    Arthur.eulerAngles = new Vector3(0, 90, 0);
                }
                else if (input < -0.1f)
                {
                    rb.velocity = new Vector3(-speed, rb.velocity.y);
                    anim.SetBool("Running", true);
                    Arthur.eulerAngles = new Vector3(0, -90, 0);
                }
                else
                {
                    rb.velocity = new Vector3(0, rb.velocity.y);
                    anim.SetBool("Running", false);
                }

                //doubleJump = true; //Makes jumping available again
                //canMove = true;


                //if (Input.GetButtonDown("Fire2"))
                //{
                //    mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
                //    dashPos = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

                //    dashing = true;
                //    dashTimer = 0;
                //    SteamManager.instance.steam -= 10; // Lowers steam by a number
                //}

                if (Input.GetButtonDown("Jump") && grounded)
                {
                    rb.AddRelativeForce(0, jumpSpeed, 0, ForceMode.Impulse);
                    gliderStarted = false;
                }

                if (Input.GetButton("Sprint") & SteamManager.instance.steamUsable == true) // Button is Shift
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
                    //canMove = true;
                    gravity = OGravity / gliderStrength; // Lowers gravity
                    speed = OSpeed;
                    glider.SetActive(true);
                }
                else if (Input.GetButtonUp("Glider"))
                {
                    gliderStarted = false;
                    gravity = OGravity;
                    glider.SetActive(false);
                }
                else
                {
                    gliderStarted = false;
                    //canMove = false;
                    gravity = OGravity;
                    glider.SetActive(false);
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
        //playerXML.velocity = rb.velocity; // No longer used //

        return playerXML;
    }

    public void SaveXML(XMLPlayer playerXML)
    {
        isMele = playerXML.isMele;
        HealthManager.instance.health = playerXML.health;
        SteamManager.instance.steam = playerXML.steam;
        bulletLoaded = playerXML.loadedAmmo;
        playerBullets = playerXML.totalAmmo;
        trans.position = playerXML.position;
        rb.velocity = playerXML.velocity; // No longer used //
    }
    

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "CollectibleAmmo")
        {
            StartCoroutine(PlayParticleEffect(ammoPE, 0.5f));
        }

        else if (other.tag == "CollectibleHealth")
        {
            StartCoroutine(PlayParticleEffect(healthPE, 0.5f));
        }
	}

    IEnumerator PlayParticleEffect(ParticleSystem ps, float duration)
    {
        ps.Play();
        yield return new WaitForSeconds(duration);
        ps.Stop();
    }

}
