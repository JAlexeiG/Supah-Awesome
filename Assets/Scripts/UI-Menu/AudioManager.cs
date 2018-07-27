using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    List <string> activeAudio;
    Scene activeScene;
    public string sceneCheck;

    //public bool playMenuTheme;
    //public bool playGameTheme;
    bool musicPlaying;

    static AudioManager _instance = null;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);

        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        activeAudio = new List<string>();
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
	}

    private void Start()
    {
        musicPlaying = false;
        activeScene = SceneManager.GetActiveScene();
        sceneCheck = activeScene.name;

        PlayTheme();
    
    }
    
    private void Update()
    {
        activeScene = SceneManager.GetActiveScene();
        if (sceneCheck != activeScene.name)
        {
            musicPlaying = false;
            StopAll();
            PlayTheme();
            sceneCheck = activeScene.name;
        }
        /*
        activeScene = SceneManager.GetActiveScene();

        if (activeScene)
        {
            Play("Menu Theme");
        }
        else if (playMenuTheme)
        {
            Play("Game Theme");
        }
        //For Testing **********************************************
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Play("Theme");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Pause("Theme");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnPause("Theme");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Stop("Theme");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Play("Mario");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Pause("Mario");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            UnPause("Mario");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Stop("Mario");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PauseAll();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            UnPauseAll();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            StopAll();
        }
        */
        //For Testing **********************************************
    }
    public void PlayTheme()
    {
        if (activeScene.name == "MainMenu")
        {
            Play("Menu Theme");
            //musicPlaying = true;
        }
        if (activeScene.name == "Main")
        {
            Play("Game Theme");
            //musicPlaying = true;
        }
        musicPlaying = true;
    }

    public void Play(string name)
    {
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        
        s.source.Play();
        activeAudio.Add(name);
    }
    void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.Pause();
        
    }
    void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.UnPause();

    }
    void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        else
        {
            activeAudio.Remove(name);
        }
        s.source.Stop();

    }

    void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Pause();
        }

    }
    void UnPauseAll()
    {
        foreach (String aud in activeAudio)
        {
            foreach (Sound sd in sounds)
            {
                if (sd.name == aud)
                {
                    sd.source.UnPause();
                }
                
            }
        }
    }
    void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
            activeAudio.Clear();
        }
    }
    public static AudioManager instance
    {
        get;
        set;
    }

}
