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
        OGravity = gravity;
        OSpeed = speed;
        ////
    }

    void Update()
    {
        //A bunch of stuff to know where the player is pointing with keys. NOT MOUSE.
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
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Creates a ray where the mouse clicks
            playerRay = Camera.main.ScreenPointToRay(gameObject.transform.position);//Creates a ray from where the character is

            //Mouse position (+20 because camera is -20) to find where to shoot something
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);


            Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

            //Explains what the mouse just clicked, if anything. Use for grappling hook if we use it
            if (Physics.Raycast(ray))
            {
                Physics.Raycast(ray, out hit); //When raycast hits something, 'hit' explains what it hit
                if (hit.transform.tag == "Enemy")
                    {
                        Destroy(hit.transform.gameObject);
                    }
            }
        }

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
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
        else
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
}
