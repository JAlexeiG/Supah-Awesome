using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static GameManager _instance = null;

    // Use this for initialization
    void Start () {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

	}

    // Update is called once per frame
    void Update() {

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game_Justin");
    }
    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
    public static GameManager instance
    {
        get;
        set;
    }
}
