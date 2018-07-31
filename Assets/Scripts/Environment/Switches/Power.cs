using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour {

    public bool isPowered;

    public void PowerSwitch()
    {
        isPowered = !isPowered;
        Debug.Log(gameObject + " power has been switched to " + isPowered);

    }

    public void PowerSwitch(bool x)
    {
        isPowered = x;
        //Debug.Log(gameObject + " power has been switched to " + x + "Using the 2nd power switch option");

    }
    
}
