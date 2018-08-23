using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour {

    public GameObject spineRotator;
    public GameObject lookAt;

    public Vector3 mousePos;
    public float cameraPos;
	
	// Update is called once per frame
	void Update () {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z + cameraPos);



        Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos);

        potato = new Vector3(potato.x, potato.y, 0);

        Debug.Log(potato);

        lookAt.transform.position = potato;

        spineRotator.transform.LookAt(lookAt.transform);

	}
}
