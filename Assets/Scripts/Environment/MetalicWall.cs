using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalicWall : MonoBehaviour
{

    Chara player;
    float gravity;


    private void Start()
    {
        player = FindObjectOfType<Chara>();
        gravity = player.gravity;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            player.gravity = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            player.gravity = gravity;
        }
    }
}