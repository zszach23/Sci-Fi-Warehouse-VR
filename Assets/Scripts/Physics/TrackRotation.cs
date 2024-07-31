using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TrackRotation : MonoBehaviour
{
    public Camera mainCamera;
    public float rotationSpeed = 2f;

    private void Update()
    {
        // Check if the camera is assigned
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not assigned in the inspector!");
            return;
        }

        // Calculate the rotation difference between the camera and the GameObject
        Quaternion cameraRotation = mainCamera.transform.rotation;
        Quaternion objectRotation = transform.rotation;
        Quaternion rotationDifference = Quaternion.Inverse(objectRotation) * cameraRotation;

        // Apply rotation to the GameObject
        transform.rotation *= Quaternion.Slerp(Quaternion.identity, rotationDifference, Time.deltaTime * rotationSpeed);
    }
}
