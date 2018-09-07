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

    public bool shooting;
    public bool bang;

    public float shootTimer;
    private void Start()
    {
        anim = GetComponent<Animator>();
        lookAtObject.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(bang)
        {
            shooting = true;
            bang = false;
            anim.SetTrigger("Shoot");
        }
        if (shooting)
        {
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, shootWeight);
            anim.SetLayerWeight(3, shootDownWeight);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
        }
        
        //Mouse position (+20 because camera is -20) to find where to shoot something
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z + cameraPosZ);


        Vector3 potato = Camera.main.ScreenToWorldPoint(mousePos); //Gives world-coordinants of where you just fired

        potato = new Vector3(potato.x, potato.y, transform.position.z);

        lookAtObject.position = potato;
        //Debug.Log(potato);

        ///Updates for aiming
        rotationObject.LookAt(lookAtObject); //Makes aim look at crosshair

        rotationCalculator();
    }

    private void rotationCalculator()
    {
        float rotatedX = rotationObject.localEulerAngles.x;
        float rotatedY = rotationObject.localEulerAngles.y;

        //Debug.Log(rotatedX + " " + rotatedY);


        if (rotatedY < 180)
        {
            if (rotatedX < 315.5f && rotatedX > 90.5f)
            {
                shootWeight = 0;
                shootDownWeight = 0;
            }
            if (rotatedX > 315)
            {
                shootWeight = (rotatedX - 315) /45;
                shootDownWeight = 0;
            }
            else if (rotatedX < 45)
            {
                shootWeight = 1;
                shootDownWeight  = (rotatedX) / 45;
            }
        }
    }

    public void changeMele(bool newMele)
    {
        isMele = newMele;
    }

    public void startShoot()
    {
        shooting = true;
    }

    public void endShoot()
    {
        shooting = false;
    }
}
