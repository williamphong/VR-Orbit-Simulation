using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonsRender : MonoBehaviour
{
    public TrailRenderer spring;
    public TrailRenderer summer;
    public TrailRenderer autumn;
    public TrailRenderer winter;

    public int dayToday;

    public OverlayFeed data;
    public SimulationController simuControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        dayToday = simuControl.getDay();
        if (dayToday < 79)
        {
            winter.emitting = true;
            autumn.emitting = false;
        }
        else if (dayToday < 79 + data.spring_len)
        {
            spring.emitting = true;
            winter.emitting = false;
        }
        else if (dayToday < 79 + data.spring_len + data.summer_len)
        {
            summer.emitting = true;
            spring.emitting = false;
        }
        else if (dayToday < 79 + data.spring_len + data.summer_len + data.fall_len)
        {
            autumn.emitting = true;
            summer.emitting = false;
        }
        else if (dayToday > 79 + data.spring_len + data.summer_len + data.fall_len)
        {
            winter.emitting = true;
            autumn.emitting = false;
        }    

    }
}
