using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followspeed =4f;
    public float smoothSpeed = 0.125f;

    void Start()
        {
            // Initialize the offset if not set
            if (offset == Vector3.zero)
            {
                offset = transform.position - target.position;
            }
        }
void LateUpdate()
    {
        if (target !=null){
            // Desired position of the camera
            Vector3 desiredPosition = target.position + offset;

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}






