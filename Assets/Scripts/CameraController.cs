using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Written by: Socrates
//Last Updated : 26/01/2018


public class CameraController : MonoBehaviour
{

    GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    bool zoom;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;

        
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (zoom)
        {
            transform.position = player.transform.position + (offset);
        }

        if(!zoom)
        {
            ZoomIn();
        }

        //for testing 
        if (Input.GetKeyDown(KeyCode.Z))
        {
            zoom = !zoom;
        }
    }
    void ZoomIn()
    {
        transform.position = player.transform.position + (offset + new Vector3(0, 0, 0));
    }
    

}
