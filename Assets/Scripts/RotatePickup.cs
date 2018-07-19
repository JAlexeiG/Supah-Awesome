using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePickup : MonoBehaviour 
{
	private void FixedUpdate()
	{
        transform.Rotate(0, Time.fixedDeltaTime * 135, 0);
	}
}
