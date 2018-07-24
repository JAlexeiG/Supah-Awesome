using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XMLSwitch : MonoBehaviour {

    public Power[] pow;
    public bool powOn;

    Power powerObject;

    private void Start()
    {
        powerObject.isPowered = powOn;
    }

    public void setPowerObject(Power x)
    {
        powerObject = x;
    }
}
