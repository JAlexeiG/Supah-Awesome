using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Returns reference to singletons
public class RefSingleton : MonoBehaviour {

    public AudioManager GetAudioManager()
    {
        //return AudioManager.instance;
        Debug.LogError("Not Setup: Once audio manager is singleton, change this function");
        return null;
    }

    public GameManager GetGameManager()
    {
        return GameManager.instance;
    }

    public XMLCheckpointManager GetCheckpointManager()
    {
        return XMLCheckpointManager.instance;
    }
}
