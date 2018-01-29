using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    static HealthManager _instance = null;

    public int maxHealth;
    public float health;
    float healthScale;
    public RectTransform healthBar;

    // Use this for initialization
    void Start ()
    {
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

        if (health <=0)
        {
            Debug.Log("You died");
            health = 5;
        }
    }
    public static HealthManager instance
    {
        get;
        set;
    }
}
