using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSave : MonoBehaviour {

    /// <summary>
    /// 
    /// This class is the actual thing you are saving. It holds the information of all XMLSwitches. The XMLSwitches will controll the save/loading of 
    /// whether or not that switch is on/off.
    /// 
    /// </summary>
    /// 
    
    public struct switchXML
    {
        public List<bool> saveSwitches;
        public List<string> nameSwitches;
    }

    //For saving
    public switchXML GetSwitch()
    {
        switchXML switchSave = new switchXML(); //Creates a new SwitchSave

        switchSave.saveSwitches = new List<bool>();
        switchSave.nameSwitches = new List<string>();

        foreach(XMLSwitch item in FindObjectsOfType<XMLSwitch>())
        {
            Debug.Log(item.name);
            switchSave.saveSwitches.Add(item.powOn);
            switchSave.nameSwitches.Add(item.name);
        }

        return switchSave;
        //Returns the new SwitchSave
    }

    public void SaveXML(switchXML savedSwitches)
    {
        //"savedSwitches" holds the save file, which will be loaded into the game
        foreach (XMLSwitch item in FindObjectsOfType<XMLSwitch>())
        {
            int i = 0;
            //Gets specific info from the save file
            foreach(string itemName in savedSwitches.nameSwitches)
            {
                //Debug.Log(itemName + " " + item.name);
                //Gets specific info from the game and checks if the object is correct
                if (item.name == itemName)
                {
                    item.powOn = savedSwitches.saveSwitches[i];
                }
                i++;
            }
        }
        //Pastes all the information from save files onto the game
    }
    
}
