using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChangeCameraAngle : MonoBehaviour {

    [SerializeField]
    bool Angled;
    [SerializeField]
    bool Fixed;


    GameObject MainCam;
    CameraController Cam;
    Vector3 location;

    BoxCollider boxCollider;

    // Use this for initialization
   
    void Start () {
        boxCollider = GetComponent<BoxCollider>();
        MainCam = GameObject.Find("Main Camera");
        Cam = MainCam.GetComponent<CameraController>();
        Cam.camNum = 1;
        location = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Angled)
            {
                Cam.camNum = 2;
            }
            else if (Fixed)
            {
                Cam.tempFixLocation = location;
                Cam.camNum = 3;
                Cam.exitingFixed = true;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
        Debug.Log("Size: " + boxCollider.bounds.size);
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Cam.camNum = 1;
        }
    }
}
