using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthFocusedCamera : MonoBehaviour
{
    public Transform earthTransform;  // The transform of the Earth object
    public Vector3 localTargetPosition;  // The local position on Earth to look at

    private void Start()
    {
        // Initialize localTargetPosition based on initial forward vector projected onto Earth's surface
        Vector3 globalTargetPoint = earthTransform.position + (transform.forward * 1000.0f);  // Large number to ensure it reaches Earth
        localTargetPosition = earthTransform.InverseTransformPoint(globalTargetPoint);
    }

    private void LateUpdate()
    {
        if (earthTransform == null)
            return;

        // Calculate the global position of the target point
        Vector3 globalTargetPosition = earthTransform.TransformPoint(localTargetPosition);

        // Adjust the camera position based on Earth's position, rotation, and current orientation
        transform.position = earthTransform.position + (transform.position - earthTransform.position).normalized * 200;  // Keep the camera a fixed distance from Earth
        transform.LookAt(globalTargetPosition);
    }
}
