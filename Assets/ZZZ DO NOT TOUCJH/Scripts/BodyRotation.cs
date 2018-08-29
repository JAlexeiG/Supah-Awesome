using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour {

    public bool isMele;

    public Transform rotationObject;
    public Transform lookAtObject;

    private Vector3 mousePos;
    private Vector3 potato;

    public float cameraPosZ;
    public float cameraPosY;

    public Animator anim;

    public Transform spine1;
    public Transform spine2;

    public float shootUpWeight;
    public float shootWeight;
    public float shootDownWeight;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {


        if(!isMele)
        {
            anim.SetLayerWeight(2, 1);
            anim.SetLayerWeight(3, shootWeight);
            anim.SetLayerWeight(4, shootDownWeight);
        }
        


        //Mouse position (+20 because camera is -20) to find where to shoot something
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z  + cameraPosZ);


        Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

        potato = new Vector3(potato.x, potato.y, transform.position.z);

        lookAtObject.position = potato;

        ///Updates for aiming
        rotationObject.LookAt(lookAtObject); //Makes aim look at crosshair

        rotationCalculator();
    }

    private void rotationCalculator()
    {
        float rotatedX = rotationObject.localEulerAngles.x;
        float rotatedY = rotationObject.localEulerAngles.y;

        Debug.Log(rotatedX + " " + rotatedY);


        if(rotatedY < 180 && rotatedY > -180)
        {
            if(rotatedX > 315)
            {
                shootWeight = (rotatedX - 315) / 45;
            }
            else if (rotatedX < 45)
            {
                shootDownWeight = rotatedX / 45;
            }
        }
    }

    public void changeMele(bool newMele)
    {
        isMele = newMele;
    }
}
