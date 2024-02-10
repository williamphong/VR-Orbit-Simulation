using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject masterControl;
    SimulationController simuControl;

    public bool paused = false;
    public float speedStorage = 1f;

    // Start is called before the first frame update
    void Start()
    {
        simuControl = masterControl.GetComponent<SimulationController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        //string myname = other.gameObject.name;
        //Debug.Log("I was touched by " + myname + "with tag " + other.gameObject.tag);

        if (other.gameObject.name.Contains("Hand") && paused == false)
        {
            speedStorage = simuControl.simulationSpeed;
            simuControl.simulationSpeed = 0f;
            paused = true;
        }
        else if (other.gameObject.name.Contains("Hand") && paused == true)
        {
            simuControl.simulationSpeed = speedStorage;
            paused = false;
        }
    }

}
