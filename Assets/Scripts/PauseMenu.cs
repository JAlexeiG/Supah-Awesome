using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    Scene activeScene;

    private void Start()
    {
        pauseMenuUI = this.gameObject;
        activeScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
                Pause();
        }
	}
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Button(string command)
    {
        switch (command)
        {
            case "Resume":
                Resume();
                break;
            case "Restart":
                SceneManager.LoadScene(activeScene.name);
                break;
            case "Menu":
                SceneManager.LoadScene(0);
                //SceneManager.LoadScene("Main"); // Either or works.
                break;
            case "Settings":
                //Does nothing at the moment.
                Debug.Log("Settings not implemented yet.");
                break;

        }
    }

    public void Restart()
    {

    }

    public void Menu()
    {

    }
}
