using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
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


    void PauseSim()
    {

        if (paused == false)
        {
            speedStorage = simuControl.simulationSpeed;
            simuControl.simulationSpeed = 0f;
            paused = true;
        }
        else if (paused == true)
        {
            simuControl.simulationSpeed = speedStorage;
            paused = false;
        }
    }


}
