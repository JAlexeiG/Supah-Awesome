using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedHelper : MonoBehaviour {
    private Chara charaScript;
    private void Start()
    {
        charaScript = FindObjectOfType<Chara>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Floor" || other.tag == "box" || other.tag == "MeleeEnemy")
        {
            charaScript.setGrounded(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor" || other.tag == "box" || other.tag == "MeleeEnemy")
        {
            charaScript.setGrounded(false);
        }
    }
}
