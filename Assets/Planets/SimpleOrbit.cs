using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOrbit : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0.2f, 0f);
    }
}

