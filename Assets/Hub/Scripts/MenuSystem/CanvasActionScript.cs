using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CanvasActionScript : MonoBehaviour
{
    // a reference to the action
    public SteamVR_Action_Boolean MenuOnOff;
// a reference to the hand
    public SteamVR_Input_Sources handType;
//reference to the Menu
    public GameObject Menu;
    // Start is called before the first frame update
    void Start()
    {
        Menu.SetActive(false);
        MenuOnOff.AddOnStateDownListener(ButtonDown, handType);
        MenuOnOff.AddOnStateUpListener(ButtonUp, handType);
    }
    public void ButtonUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Button is up");
        Menu.SetActive(false);
    }
    public void ButtonDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Button is down");
        Menu.SetActive(true);
    }

    void Update()
    {
        
    }
}