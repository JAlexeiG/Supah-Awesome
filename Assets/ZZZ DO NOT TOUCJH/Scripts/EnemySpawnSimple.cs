using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnSimple : MonoBehaviour {

    public bool trigger;

    [SerializeField]
    private GameObject MelePref;

    [SerializeField]
    private GameObject RangedPref;
    
    [SerializeField]
    public GameObject SpoodersPref;

    [System.Serializable]
    public class spooderInfo
    {
        public Transform Spooders;
        
        public Transform MovePos1;
        
        public Transform MovePos2;
    }

    [SerializeField]
    public spooderInfo[] spooderInf;
    
    
    [SerializeField]
    private Transform[] Mele;

    [SerializeField]
    private Transform[] Ranged;

    
    private GameObject[] melePos;
    
    private GameObject[] rangedPos;
    
    private GameObject[] spooderPos;

    
    // Use this for initialization
    void Start ()
    {
        melePos = new GameObject[Mele.Length];
        rangedPos = new GameObject[Ranged.Length];
        spooderPos = new GameObject[spooderInf.Length];
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
        for (int i = 0; i < spooderInf.Length && spooderInf.Length > 0; i++)
        {
            GameObject newSpooder = Instantiate(SpoodersPref, spooderInf[i].Spooders.position, spooderInf[i].Spooders.rotation);
            SpiderAI spiderAI = newSpooder.GetComponentInChildren<SpiderAI>();
            spiderAI.setPos(spooderInf[i].MovePos1, spooderInf[i].MovePos2);
            spooderPos[i] = newSpooder;
            spiderAI.start();
        }
    }
}
