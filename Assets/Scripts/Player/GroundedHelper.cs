using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class GroundedHelper : MonoBehaviour {
    private Chara charaScript;
    [SerializeField]
    private Animator anim;
    private void Start()
    {
        charaScript = FindObjectOfType<Chara>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Floor" || other.tag == "box" || other.tag == "MeleeEnemy")
        {
            charaScript.setGrounded(true);
            anim.SetBool("Grounded", true);
            anim.SetTrigger("Landed");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor" || other.tag == "box" || other.tag == "MeleeEnemy")
        {
            charaScript.setGrounded(false);
            anim.SetBool("Grounded", false);
            anim.SetTrigger("Falling");
            anim.ResetTrigger("Landed");
        }
    }
}
