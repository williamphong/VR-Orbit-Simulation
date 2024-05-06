using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject player;
    public GameObject hub;


    private float toggleCooldown = 0f;
    public bool UIToggled = false;
    public GameObject trail;

    public GameObject earthLatLong;

    public Transform hubSpot;
    public Transform freeRoamSpot;

    public GameObject freeRoamZone;


    public void ToggleUI()
    {
        if(Time.time > toggleCooldown)
        {
            toggleCooldown = Time.time + 0.5f;
            UIToggled = !UIToggled;
            trail.SetActive(UIToggled);
            earthLatLong.SetActive(UIToggled);
        }
    }

    public void TeleportHome()
    {
        hub.SetActive(true);
        freeRoamZone.SetActive(false);
        player.transform.position = hubSpot.position;
    }

    public void FreeRoam()
    {
        hub.SetActive(false);
        freeRoamZone.SetActive(true);
        player.transform.position = freeRoamSpot.position;
    }
}
