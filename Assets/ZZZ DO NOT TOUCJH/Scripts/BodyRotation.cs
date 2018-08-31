using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour {

    public bool isMele;

    public Transform rotationObject;
    public Transform lookAtObject;

    private Vector3 mousePos;

    public float cameraPosZ;

    public Animator anim;

    public float shootUpWeight;
    public float shootWeight;
    public float shootDownWeight;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        anim.SetLayerWeight(2, 1);
        anim.SetLayerWeight(3, shootWeight);
        anim.SetLayerWeight(4, shootDownWeight);



        //Mouse position (+20 because camera is -20) to find where to shoot something
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z + cameraPosZ);


        Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

        potato = new Vector3(potato.x, potato.y, transform.position.z);

        lookAtObject.position = potato;
        Debug.Log(potato);

        ///Updates for aiming
        rotationObject.LookAt(lookAtObject); //Makes aim look at crosshair

        rotationCalculator();
    }

    private void rotationCalculator()
    {
        float rotatedX = rotationObject.localEulerAngles.x;
        float rotatedY = rotationObject.localEulerAngles.y;

        Debug.Log(rotatedX + " " + rotatedY);


        if (rotatedY < 91 && rotatedY > 89)
        {
            if (rotatedX > 45 && rotatedX < 270)
            {
                shootDownWeight = (-rotatedX + 90) /45;
            }
        }
        else if (rotatedY < 210 && rotatedY > 269)
        {
            if (rotatedX > 45 && rotatedX < 270)
            {
                shootWeight = (rotatedX - 45) / 45;
            }
        }
    }

    public void changeMele(bool newMele)
    {
        isMele = newMele;
    }
}
