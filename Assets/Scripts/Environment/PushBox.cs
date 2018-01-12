using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour {

    public float pushStrength = 4.0f;
    private CharacterController character;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }
        if(hit.moveDirection.y < -0.3f)
        {
            return;
        }
        Vector3 direction = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = direction * pushStrength;
    }
}
