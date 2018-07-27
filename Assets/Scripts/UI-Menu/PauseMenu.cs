﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsPanel;
    public AudioManager audioManager;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        settingsPanel.SetActive(false);
        audioManager = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape is pressed.");
            if (GameIsPaused)
            {
                Resume();
                //audioManager.Play("Click");     pause sound
            }
            else
            {
                Pause();
                //audioManager.Play("Click");     pause sound
            }

        }
	}
    public void Resume()
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

    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
        Resume();
    }


    public void Menu()
    {
        Resume();
        SceneManager.LoadSceneAsync(0);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }
    public void Back()
    {
        settingsPanel.SetActive(false);
    }
}
