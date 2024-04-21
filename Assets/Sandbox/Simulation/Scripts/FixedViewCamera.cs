using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedViewCamera : MonoBehaviour
{
    public Transform earthTransform; // Assign the Earth's transform here in the inspector
    private Quaternion initialRelativeRotation;

    void Start()
    {
        // Capture the initial relative rotation from the camera to the Earth
        initialRelativeRotation = Quaternion.Inverse(earthTransform.rotation) * transform.rotation;
    }

    void Update()
    {
        // Update the camera's rotation to maintain the initial relative rotation as the Earth rotates
        transform.rotation = earthTransform.rotation * initialRelativeRotation;
    }
}
