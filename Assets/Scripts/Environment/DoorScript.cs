using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Power
{

    [SerializeField]
    GameObject piston;

    [SerializeField]
    Transform position1;
    [SerializeField]
    Transform position3;

    Transform newPosition;

    [SerializeField]
    float toPos1Speed;
    [SerializeField]
    float toPos3Speed;
    float newSpeed;

    int state;

    // Use this for initialization
    void Start()
    {
        state = 1;

        MoveToFirstPosition();
    }

    void FixedUpdate()
    {
        if (state == 1)
        {
            newPosition = position1;
            newSpeed = toPos1Speed;
        }
        
        else if (state == 3)
        {
            newPosition = position3;
            newSpeed = toPos3Speed;
        }

        if (isPowered)
        {
            piston.transform.position = Vector3.MoveTowards(piston.transform.position, newPosition.position, newSpeed * Time.fixedDeltaTime);
        }

        else
        {
            piston.transform.position = Vector3.MoveTowards(piston.transform.position, position1.position, toPos1Speed * Time.deltaTime);
        }

    }

    void MoveToFirstPosition()
    {
        state = 3;
        Invoke("MovetoSecondPosition", 5f);
    }
    
    void MoveToThirdPosition()
    {
        state = 1;
        Invoke("MoveToFirstPosition", 2f);
    }
}
