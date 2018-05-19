using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamManager : MonoBehaviour {

    static SteamManager _instance = null;

    public bool isPaused;
    public bool steamUsable = true;

    public float steam;
    float steamScale;
    public RectTransform steamBar;
    // Use this for initialization
    void Start () {
        isPaused = false;
        instance = this;
        if (steam <= 0)
        {
            steam = 50;
        }
        steamScale = steamBar.sizeDelta.x / steam;
    }
	
	// Update is called once per frame
	void Update ()
    {


        steamBar.sizeDelta = new Vector2(steam * steamScale, steamBar.sizeDelta.y); //Adjusts the length of the steam bar


        //If the steam is less than 0, it goes on cooldown. It also regenerates slowly.
        if (!isPaused)
        {
            if (steam < 50)
            {
                steam += 0.5f;
            }

            if (steam <= 0)
            {
                steamUsable = false;
            }
            if (steam >= 50)
            {
                steamUsable = true;
            }
        }
    }

    public static SteamManager instance
    {
        get;
        set;
    }


    void OnTriggerEnter(Collider col)
    {
        col.transform.parent = gameObject.transform;
    }
    void OnTriggerExit(Collider col)
    {
        col.transform.parent = null;
    }
}
