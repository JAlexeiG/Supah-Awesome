using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by: Socrates
//Start Date: Who Knows?
//Last Updated: 16/02/2018
//Finished Date: 

/*
 Camera Settings:
*All positions are player transform plus the veriables below (x,y,z)*

Wide Screen:(For Testing) 0
position(4,6.5,-20)
rotation(15,0,0)
scale(1,1,1)

Default: 1
position(4,6.5,-10)
rotation(15,0,0)
scale(1,1,1)

Angled: 2
position(2,6.5,-10)
rotation(15,25,0)
scale(1,1,1)

Fixed: 3
position(10,6.5,-15)
rotation(15,0,0)
scale(1,1,1)
*/

public class CameraController : MonoBehaviour
{

    GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    Vector3 rOffset;

    //Position
    Vector3 pDefault = new Vector3(4, 4, -10);
    Vector3 pWideScreen = new Vector3(4, 4, -20);
    Vector3 pAngled = new Vector3(2, 4, -10);
    Vector3 pFixed = new Vector3(10, 4, -15);

    //Rotation
    Vector3 rDefault = new Vector3(15, 0, 0);
    Vector3 rWideScreen = new Vector3(15, 0, 0);
    Vector3 rAngled = new Vector3(15, 25, 0);
    Vector3 rFixed = new Vector3(15, 0, 0);

    public enum Cam {WideScreen,Default,Angled,Fixed};
    public int camNum;

    Cam PlayerCam;
    Cam SetCam;

    bool zoom;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        
        //offset = pDefault - player.transform.position;
        offset = pDefault;

        //rotation
        transform.rotation = Quaternion.Euler(15, 0, 0);

        PlayerCam = Cam.Default;
        SetCam = PlayerCam;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        
        transform.position = player.transform.position + offset;
        //transform.Rotate(rDefault);

        if (camNum == 0)
        {
            PlayerCam = Cam.WideScreen;
        }
        else if (camNum == 1)
        {
            PlayerCam = Cam.Default;
        }
        else if (camNum == 2)
        {
            PlayerCam = Cam.Angled;
        }
        else if (camNum == 3)
        {
            PlayerCam = Cam.Fixed;
        }

        if (SetCam != PlayerCam)
        {
            CameraType(PlayerCam);
            SetCam = PlayerCam;
        }

        //For TESTING***
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerCam = Cam.WideScreen;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerCam = Cam.Default;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerCam = Cam.Angled;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerCam = Cam.Fixed;
        }
        //For TESTING***

    }
    void CameraType(Cam type)
    {
        switch (type)
        {
            case Cam.WideScreen:
                offset = pWideScreen ;
                transform.rotation = Quaternion.Euler(15, 0, 0);
                break;

            case Cam.Default:
                offset = pDefault ;
                transform.rotation = Quaternion.Euler(15, 0, 0);
                break;

            case Cam.Angled:
                offset = pAngled ;
                transform.rotation =  Quaternion.Euler(15, 25, 0);
                break;

            case Cam.Fixed:
                offset = pFixed ;
                transform.rotation = Quaternion.Euler(15, 0, 0);
                break;


        }
    }
   
    
    

}
