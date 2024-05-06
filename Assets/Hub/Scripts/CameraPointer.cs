using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointer : MonoBehaviour
{

    public Transform earthCenter;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(earthCenter);
    }

}
