using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePickup : MonoBehaviour 
{
    [SerializeField] float rotateSpeed;

	private void Awake()
	{
        if (rotateSpeed <= 0)
            rotateSpeed = 135;
	}

	private void FixedUpdate()
	{
        transform.Rotate(0, 0, Time.fixedDeltaTime * rotateSpeed);
	}
}
