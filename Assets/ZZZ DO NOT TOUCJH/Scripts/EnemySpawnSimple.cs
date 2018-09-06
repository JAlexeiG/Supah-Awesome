using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSimple : MonoBehaviour {

    public bool trigger;

    [SerializeField]
    private GameObject MelePref;

    [SerializeField]
    private GameObject RangedPref;

    [SerializeField]
    private GameObject SpoodersPref;

    [SerializeField]
    private Transform[] Mele;

    [SerializeField]
    private Transform[] Ranged;

    [SerializeField]
    private Transform[] Spooders;
    [SerializeField]
    private Transform[] Spooders;


    private GameObject[] melePos;
    
    private GameObject[] rangedPos;
    
    private GameObject[] spooderPos;

    
    // Use this for initialization
    void Start ()
    {
        melePos = new GameObject[Mele.Length];
        rangedPos = new GameObject[Ranged.Length];
        spooderPos = new GameObject[Spooders.Length];
    }

    private void Update()
    {
        if(trigger)
        {
            spawnLevel();
            trigger = false;
        }
    }
    public void spawnLevel()
    {
        for (int i = 0; i < Mele.Length && Mele.Length > 0; i++)
        {
            GameObject newMele = Instantiate(MelePref, Mele[i].position, Mele[i].rotation);
            melePos[i] = newMele;
        }
        for (int i = 0; i < Ranged.Length && Ranged.Length > 0; i++)
        {
            GameObject newRanged = Instantiate(RangedPref, Ranged[i].position, Ranged[i].rotation);
            rangedPos[i] = newRanged;
        }
        for (int i = 0; i < Spooders.Length && Spooders.Length > 0; i++)
        {
            GameObject newSpooder = Instantiate(SpoodersPref, Spooders[i].position, Spooders[i].rotation);
            spooderPos[i] = newSpooder;
        }
    }
}
