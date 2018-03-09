using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by: Socrates
//Start Date: Who Knows?
//Last Updated: 26/02/2018
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

    //Position
    Vector3 pDefault = new Vector3(4, 4, -10);
    Vector3 pWideScreen = new Vector3(4, 4, -20);
    Vector3 pAngled = new Vector3(2, 4, -10);
    Vector3 pFixed /*= new Vector3(10, 4, -15)*/;

    //Rotation
    Quaternion rDefault = Quaternion.Euler(15, 0, 0);
    Quaternion rWideScreen = Quaternion.Euler(15, 0, 0);
    Quaternion rAngled = Quaternion.Euler(15, 25, 0);
    Quaternion rFixed = Quaternion.Euler(15, 0, 0);

    public enum Cam {WideScreen,Default,Angled,Fixed,Backward};
    public int camNum;

    Cam PlayerCam;
    Cam SetCam;

    public float transitionSpeed =2f;

    public Vector3 tempFixLocation;

    public bool exitingFixed = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        
        //offset = pDefault - player.transform.position;
        offset = pDefault;

        //rotation
        transform.rotation = Quaternion.Euler(15, 0, 0);

        //currentView = tDefault;

        

        PlayerCam = Cam.WideScreen;
        SetCam = PlayerCam;

    }
    void Update()
    {
        
        switch (PlayerCam)
        {
            case Cam.WideScreen:
                //currentView = tWideScreen;
                offset = Vector3.Lerp(offset, pWideScreen, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, rWideScreen, Time.deltaTime * transitionSpeed);
                break;

            case Cam.Default:
                offset = Vector3.Lerp(offset, pDefault, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, rDefault, Time.deltaTime * transitionSpeed);
                break;

            case Cam.Angled:
                offset = Vector3.Lerp(offset, pAngled, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, rAngled, Time.deltaTime * transitionSpeed);
                break;

            case Cam.Fixed:
                //currentView = tFixed;
                pFixed = tempFixLocation;
                offset = Vector3.Lerp(transform.position, pFixed, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, rFixed, Time.deltaTime * transitionSpeed);
                break;


        }
        if (exitingFixed)
        {
            transitionSpeed = 90f;
            //exitingFixed = false;
        }
    }

        // LateUpdate is called after Update each frame
        void LateUpdate()
    {
        if (!exitingFixed)
        {
            transitionSpeed = 2f;
            
        }
        //currentView = tDefault;
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.

        if (PlayerCam != Cam.Fixed)
        {
            
            transform.position = player.transform.position + offset;
            transitionSpeed = 2f;
        }
        else 
        {
            transform.position = offset;
        }
        


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

        /*
        if (SetCam != PlayerCam)
        {
            //CameraType(PlayerCam);
            
            //SetCam = PlayerCam;
        }
        */

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
    /*
    void CameraType(Cam type)
    {
        switch (type)
        {
            case Cam.WideScreen:
                //currentView = tWideScreen;
                offset = pWideScreen ;
                transform.rotation = Quaternion.Euler(15, 0, 0);
                break;

            case Cam.Default:
                //currentView = tDefault;
                offset = pDefault;
                //transform.rotation = Quaternion.Euler(15, 0, 0);
                break;

            case Cam.Angled:
                //currentView = tAngled;
                offset = pAngled;
                //offset = Vector3.Lerp(offset,pAngled,Time.deltaTime * transitionSpeed) ;
                //Quaternion target = Quaternion.Euler(15, 25, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * transitionSpeed);
                //transform.rotation =  Quaternion.Euler(15, 25, 0);
                break;

            case Cam.Fixed:
                //currentView = tFixed;
                offset = pFixed;
                transform.rotation = Quaternion.Euler(15, 0, 0);
                break;


        }
    }
    */
    /*  
  //Lerp position
  transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

  Vector3 currentAngle = new Vector3 (
   Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
   Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
   Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

  transform.eulerAngles = currentAngle;
  
     
     
      Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
     */


}
