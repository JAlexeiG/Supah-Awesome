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
        if(other.tag == "Floor")
        {
            charaScript.setGrounded(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            charaScript.setGrounded(false);
        }
    }
}
