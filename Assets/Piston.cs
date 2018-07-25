using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour {

    [SerializeField]
    GameObject piston;

    [SerializeField]
    Transform position1;
    [SerializeField]
    Transform position2;
    [SerializeField]
    Transform position3;

    float toPos1Speed;
    float toPos2Speed;
    float toPos3Speed;

	// Use this for initialization
	void Start () 
    {
        toPos1Speed = 3;
        toPos2Speed = 0.5f;
        toPos3Speed = 8f;

        StartCoroutine("MoveToFirstPosition");
	}

    void MoveToFirstPosition()
    {
        Debug.Log("Moving to first position");
        piston.transform.position = Vector3.MoveTowards(piston.transform.position, position1.position, toPos1Speed * Time.deltaTime);
        Invoke("MovetoSecondPosition", 4f);
    }

    void MovetoSecondPosition()
    {
        Debug.Log("Moving to second position");
        piston.transform.position = Vector3.MoveTowards(piston.transform.position, position2.position, toPos2Speed * Time.deltaTime);
        Invoke("MoveToThirdPosition", 4f);
    }

    void MoveToThirdPosition()
    {
        Debug.Log("Moving to third position");
        piston.transform.position = Vector3.MoveTowards(piston.transform.position, position3.position, toPos3Speed * Time.deltaTime);
        Invoke("MoveToFirstPosition", 2f);
    }
}
