using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeasonsRender : MonoBehaviour
{
    public TrailRenderer spring;
    public TrailRenderer summer;
    public TrailRenderer autumn;
    public TrailRenderer winter;

    private bool isSpring = false, springSet = false;
    private bool isSummer = false, summerSet = false;
    private bool isAutumn = false, autumnSet = false;
    private bool isWinter = false, winterSet = false;

    private int dayToday;

    public OverlayFeed data;
    public SimulationController simuControl;

    // Update is called once per frame
    void Update()
    {
        dayToday = simuControl.getDay();
        if ((dayToday < 79 || dayToday > 79 + data.spring_len + data.summer_len + data.fall_len))
        {
            isWinter = true;
            isSpring = false;
            isSummer = false;
            isAutumn = false;
        }
        else if (dayToday < 79 + data.spring_len)
        {
            isWinter = false;
            isSpring = true;
            isSummer = false;
            isAutumn = false;
        }
        else if (dayToday < 79 + data.spring_len + data.summer_len)
        {
            isWinter = false;
            isSpring = false;
            isSummer = true;
            isAutumn = false;
        }
        else if (dayToday < 79 + data.spring_len + data.summer_len + data.fall_len)
        {
            isWinter = false;
            isSpring = false;
            isSummer = false;
            isAutumn = true;
        }


        if (isWinter && !winterSet)
        {
            winterSet = true;
            springSet = false;
            summerSet = false;
            autumnSet = false;
            winter.emitting = true;
            autumn.emitting = false;
            summer.emitting = false;
            spring.emitting = false;
        }
        else if (isSpring && !springSet)
        {
            winterSet = false;
            springSet = true;
            summerSet = false;
            autumnSet = false;
            winter.emitting = false;
            autumn.emitting = true;
            summer.emitting = false;
            spring.emitting = false;
        }
        else if (isSummer && !summerSet)
        {
            winterSet = false;
            springSet = false;
            summerSet = true;
            autumnSet = false;
            winter.emitting = false;
            autumn.emitting = false;
            summer.emitting = true;
            spring.emitting = false;
        }
        else if (isAutumn && !autumnSet)
        {
            winterSet = false;
            springSet = false;
            summerSet = false;
            autumnSet = true;
            winter.emitting = false;
            autumn.emitting = false;
            summer.emitting = false;
            spring.emitting = true;
        }
    }
}
