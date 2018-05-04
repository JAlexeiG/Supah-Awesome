using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : Power {

    private Vector2 startPostition;
    private Vector2 newPosition;
    [SerializeField]
    private int speed = 3;
    [SerializeField]
    private int maxDistance = 3;

	// Use this for initialization
	void Start () {
        startPostition = transform.position;
        newPosition = transform.position;
	}
    void FixedUpdate()
    {
    float time = Time.time;

        if (isPowered)
        {
            newPosition.x = startPostition.x + (maxDistance * Mathf.Sin(time * speed));
            transform.position = newPosition;
        }

    }
    // Update is called once per frame
    void Update () {

	}
}
