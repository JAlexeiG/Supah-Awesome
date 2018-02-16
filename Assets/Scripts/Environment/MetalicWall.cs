using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalicWall : MonoBehaviour
{

    Chara player;
    Transform trans;
    Vector3 normal;


    private void Start()
    {
        trans = GetComponent<Transform>();
        player = FindObjectOfType<Chara>();
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.name == "Player")
        {
            player.angle = trans.eulerAngles.z;
            player.onWall = true;
            Debug.Log("Player has hit the wall");
        
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag + "Left");
        if (other.name == "Player")
        {
            player.onWall = false;
            player.angle = 0.0f;
            Debug.Log("Player has left the wall");
        }
    }
    
}