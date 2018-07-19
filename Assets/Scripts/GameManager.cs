using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static GameManager _instance = null;

    [SerializeField]
    private bool loadOn;

    public class CurrentScene
    {
        public int sceneNumber;
    }

    public CurrentScene GetScene()
    {
        CurrentScene scene = new CurrentScene();
        scene.sceneNumber = SceneManager.GetActiveScene().buildIndex;
        return scene;
    }

    public void loadSave(CurrentScene XMLScene)
    {
        SceneManager.LoadScene(XMLScene.sceneNumber);
    }


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

    public void LoadJustin()
    {
        SceneManager.LoadScene("Game_Justin");
    }
    public void LoadAlpha(int num)
    {
        SceneManager.LoadScene(num);
    }
    public void LoadCam()
    {
        SceneManager.LoadScene("Tim with Cam");
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
