using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTester : MonoBehaviour {

    public Animator anim;

    public bool grounded;

    public bool walking;
    
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("isGrounded", !grounded);
        anim.SetBool("Walking", walking);
	}
}
