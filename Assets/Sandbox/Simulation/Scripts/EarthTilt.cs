using UnityEngine;

public class EarthTilt : MonoBehaviour
{
    void Start()
    {
        // Set the rotation to 23.5 degrees around the X-axis
        transform.rotation = Quaternion.Euler(23.5f, 0f, 0f);
    }
}