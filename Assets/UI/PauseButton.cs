using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    public GameObject earth;
    TimeOrbit earthOrbit;
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        earthOrbit = earth.GetComponent<TimeOrbit>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        string myname = other.gameObject.name;
        Debug.Log("I was touched by " + myname + "with tag " + other.gameObject.tag);

        if (other.gameObject.name.Contains("Hand") && paused == false)
        {
            earthOrbit.orbitSpeed = 0f;
            paused = true;
        }
        else if (other.gameObject.name.Contains("Hand") && paused == true)
        {
            earthOrbit.orbitSpeed = 0.5f;
            paused = false;
        }
    }

}
