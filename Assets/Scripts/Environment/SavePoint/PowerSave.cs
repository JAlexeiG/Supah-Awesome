using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSave : MonoBehaviour {

    public Power[] powerObjects;
    
    public struct powerOn
    {
        public List<bool> powOn;
    }

    public powerOn GetPower()
    {
        powerOn power = new powerOn();
        power.powOn = new List<bool>();
        foreach (Power item in powerObjects)
        {
            power.powOn.Add(item.isPowered);
        }

        return power;
    }

    public void SaveXMLPlayer(powerOn power)
    {
        int i = 0;
        foreach (Power item in powerObjects)
        {
            item.isPowered = power.powOn[i];
        }
    }
}
