using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Transform movingPlatform;
	public Transform position1;
	public Transform position2;
	public Vector3 newposition;
	public string currentState;
	public float smooth;
	public float resetTime;

    public bool isPowered;

    // Use this for initialization
    void Start () {
        ChangeTarget();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isPowered)
        {
            movingPlatform.position = Vector3.MoveTowards(movingPlatform.position, newposition, smooth * Time.deltaTime);
        }

    }
	public void ChangeTarget()
	{
		if (currentState == "Moving to Position 1")
		{
			currentState = "Moving to Position 2";
			newposition = position2.position;
		}
		else if (currentState == "Moving to Position 2")
		{
			currentState = "Moving to Position 1";
			newposition = position1.position;
		}
		else if (currentState == "")
		{
			currentState = "Moving to Position 2";
			newposition = position2.position;
		}
		Invoke("ChangeTarget", resetTime);
	}

    public void PowerSwitch()
    {
        isPowered = !isPowered;
        Debug.Log(gameObject + " power has been switched to " + isPowered);

    }
}
