using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour {

    static HealthManager _instance = null;

    public int maxHealth;
    public float health;
    float healthScale;
    public RectTransform healthBar;

    private Transform playerTrans;

    [SerializeField]
    Transform SpawnPoint;

    [SerializeField]
    public GameObject gameover;

    // Use this for initialization
    void Start ()
    {
        playerTrans = FindObjectOfType<Chara>().gameObject.transform;

        foreach (Transform trans in playerTrans)
        {
            //Debug.Log(trans.name);
        }
        instance = this;
        if (health <= 0)
        {
            health = 100;
        }
        if (maxHealth <= 0)
        {
            maxHealth = 100;
        }
        healthScale = healthBar.sizeDelta.x / health;

    }
	
	// Update is called once per frame
	void Update ()
    {
        healthBar.sizeDelta = new Vector2(health * healthScale, healthBar.sizeDelta.y);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <=0)
        {
            gameover.SetActive(true);

            //respawn();
        }
    }

    public void respawn()
    {
        //gameover.SetActive(true);
        if (Directory.Exists("SaveFiles"))
        {
            XMLCheckpointManager.instance.load();
        }
        else
        {
            playerTrans.position = SpawnPoint.position;
            health = 100;
        }
    }
    public static HealthManager instance
    {
        get;
        set;
    }
    
}
