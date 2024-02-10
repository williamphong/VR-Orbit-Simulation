using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public GameObject masterControl;
    SimulationController simuControl;

    public float currentTime = 0f;
    public float orbitDuration = 365f;
    public float xRadius = 50f;
    public float yRadius = 40f;

    // Start is called before the first frame update
    void Start()
    {
        simuControl = masterControl.GetComponent<SimulationController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * simuControl.simulationSpeed;

        transform.localPosition = getPosition(currentTime);

        if (currentTime >= orbitDuration) currentTime = 0;
        
    }


    Vector3 getPosition(float time)
    {
        
        Vector3 position = new Vector3(xRadius * Mathf.Cos(2 * Mathf.PI * time / orbitDuration), 0.0f, yRadius * Mathf.Sin(2 * Mathf.PI * time / orbitDuration));

        return position;
    }

}


