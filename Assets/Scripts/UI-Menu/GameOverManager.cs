using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    public float restartDelay = 5f;
    public HealthManager health;
    Animator anim;
    float restartTimer;



	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (HealthManager.instance.health <= 0)
        {
            anim.SetTrigger("GameOver");
            restartTimer += Time.deltaTime;

            if(restartTimer >= restartDelay)
            {
                SceneManager.LoadSceneAsync(1);
            }
        }
	}
}
