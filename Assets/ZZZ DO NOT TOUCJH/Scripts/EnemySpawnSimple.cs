using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnSimple : MonoBehaviour {

    public bool trigger;
    public bool triggerCheck;

    [SerializeField]
    private GameObject[] meleObjects;

    [SerializeField]
    private GameObject[] rangedObjects;
    [SerializeField]
    private GameObject[] spooderObjects;


    [SerializeField]
    private GameObject[] anyExtraObjects;


    // Use this for initialization
    void Start ()
    {
        triggerCheck = trigger;
        for (int i = 0; i < meleObjects.Length && meleObjects.Length > 0; i++)
        {
            meleObjects[i].SetActive(false);
        }
        for (int i = 0; i < rangedObjects.Length && rangedObjects.Length > 0; i++)
        {
            rangedObjects[i].SetActive(false);
        }
        for (int i = 0; i < spooderObjects.Length && spooderObjects.Length > 0; i++)
        {
            spooderObjects[i].SetActive(false);
        }
        for (int i = 0; i < anyExtraObjects.Length && anyExtraObjects.Length > 0; i++)
        {
            anyExtraObjects[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (trigger != triggerCheck)
        {
            switchOn(trigger);
        }
    }

    public void switchOn(bool switching)
    {
        for (int i = 0; i < meleObjects.Length && meleObjects.Length > 0; i++)
        {
            meleObjects[i].SetActive(switching);
        }
        for (int i = 0; i < rangedObjects.Length && rangedObjects.Length > 0; i++)
        {
            rangedObjects[i].SetActive(switching);
        }
        for (int i = 0; i < spooderObjects.Length && spooderObjects.Length > 0; i++)
        {
            spooderObjects[i].SetActive(switching);
        }
        for (int i = 0; i < anyExtraObjects.Length && anyExtraObjects.Length > 0; i++)
        {
            anyExtraObjects[i].SetActive(switching);
        }
        triggerCheck = trigger;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            trigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger = false;
        }
    }
}
