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
        return TimeConverter.FromFloat((double)currentTime + 186, true);
    }

    public int getDay()
    {
        DateTime epoch = new DateTime(1970, 7, 5);
        DateTime resultDateTime;

        resultDateTime = epoch.AddDays(currentTime);

        return resultDateTime.DayOfYear;
    }

    public int getYear(bool isDays = true)
    {
        DateTime epoch = new DateTime(1970, 7, 5);
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
