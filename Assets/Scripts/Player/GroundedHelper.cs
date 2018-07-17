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
            charaScript.setGrounded(true);
    }
    private void OnTriggerExit(Collider other)
    {
            charaScript.setGrounded(false);
    }
}
