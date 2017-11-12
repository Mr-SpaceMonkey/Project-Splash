﻿using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public PlayerMotor pm;

    public float smoothSpeed;
    public float distanceAway;
    public float distanceUp;
    public Vector3 offset;
    Vector3 targetPosition;

    void FixedUpdate()
    {
        if (pm.isGrounded)
        {
            targetPosition = target.position + target.up * distanceUp - target.forward * distanceAway;
            //targetPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            transform.LookAt(target);
        }
        else
        {
            //targetPosition = target.position + target.up * distanceUp - target.forward * distanceAway;
            //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            targetPosition = target.position + Vector3.up * distanceUp - target.forward * distanceAway;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            transform.LookAt(target);
        }
    }

}
