using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour 
{
    [SerializeField]AnimationClip walkAnim;
    [SerializeField]AnimationClip idleAnim;

    [SerializeField]Animator animator;

    Vector3 oldLoc;
    Vector3 newLoc;

	// Use this for initialization
	void Start () 
    {
        animator = GetComponent<Animator>();
        StartCoroutine("UpdatePosition");
        StartCoroutine("CheckPosition");
        newLoc = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    IEnumerator CheckPosition()
    {
        if (Vector3.Distance(oldLoc, newLoc) < 2)
        {
            animator.SetBool("walking", false);
        }
        else
            animator.SetBool("walking", true);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("CheckPosition");
    }

    IEnumerator UpdatePosition()
    {
        oldLoc = newLoc;
        newLoc = transform.position;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("UpdatePosition");
    }
}
