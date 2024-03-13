using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public GameObject masterControl;
    SimulationController simuControl;

    public OverlayFeed dataFeed;

    public float currentTime = 0f;
    public float orbitDuration = 365f;

    public bool constantOrbit = true;

    public float radius = 50f;

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

        transform.localPosition = getPosition(simuControl.currentTime);

    }


    Vector3 getPosition(float time)
    {

        Vector3 position = new Vector3(xRadius * Mathf.Cos(2 * Mathf.PI * time / orbitDuration), 0.0f, yRadius * Mathf.Sin(2 * Mathf.PI * time / orbitDuration));

        if (constantOrbit == false) position = new Vector3(radius * Mathf.Cos(2 * Mathf.PI * time / orbitDuration), 0.0f, radius * (1-(float)dataFeed.eccentricity) * Mathf.Sin(2 * Mathf.PI * time / orbitDuration));

        return position;
    }

}


