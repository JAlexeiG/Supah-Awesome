using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTestScript : MonoBehaviour {
    public bool trigger;
    public Animator charaAnim;
    public Animator gunAnim;

    private void Update()
    {
        if(trigger)
        {
            gunAnim.SetTrigger("Shoot");
            charaAnim.SetTrigger("Shoot");
            trigger = false;
        }
    }

}
