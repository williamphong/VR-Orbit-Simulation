using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeConversion;
using System;


public class SimulationController : MonoBehaviour
{

    public float simulationSpeed = 1f;
    public float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * simulationSpeed;

    }

    public string dateRead()
    {
        return TimeConverter.FromFloat((double)currentTime, true);
    }

    public int getYear(bool isDays = true)
    {
        DateTime epoch = new DateTime(1970, 1, 1);
        DateTime resultDateTime;

        if (isDays)
        {
            resultDateTime = epoch.AddDays(currentTime);
        }
        else
        {
            resultDateTime = epoch.AddSeconds(currentTime);
        }

        return resultDateTime.Year;
    }
}
