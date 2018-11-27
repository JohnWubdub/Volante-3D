using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour //camera follow behavior
{

    public Transform plane;

    public float smoothSpeed = 0.125f;
    public float smoothRotateSpeed = 10f;
    public Vector3 offset; //offset for the camera angle

    void Start ()
    {
		
	}
	
	void FixedUpdate ()
    {
        Vector3 desiredPosition = plane.position + offset; //position of the plane and the angle the camera should be at, creating the smooth effect
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); //the lerp to the correct position
        transform.position = smoothedPosition; //executes the lerp

        float desiredRotation = plane.eulerAngles.y;
        float currentRotation = transform.eulerAngles.y;
        
        currentRotation = Mathf.Lerp(currentRotation, desiredRotation, Time.deltaTime * smoothRotateSpeed);
        var updatedRotation = Quaternion.Euler(0, currentRotation, 0);
        transform.rotation = updatedRotation;



        transform.LookAt(plane); //angles always at the plane no matter what the plane is doing


    }
}
