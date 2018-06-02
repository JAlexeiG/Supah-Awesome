using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMovementManager : MonoBehaviour {
    [SerializeField]
    private Transform newPosition;

    [SerializeField]
    private bool startPos;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (startPos)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    other.transform.position = newPosition.position;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    other.transform.position = newPosition.position;
                }
            }
        }
    }
}
