using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture1 : MonoBehaviour
{
    public GameObject masterControl;
    SimulationController simuControl;

    public GameObject player;

    AudioSource lecturer;

    public GameObject focus;
    Renderer focusRender;

    public GameObject orbitTrail;
    Renderer orbitRender;

    bool lectureOn = false;
    Vector3 playerPositionStorage;
    Vector3 podiumPositionStorage;
    //Quaternion podiumRotationStorage;
    float speedStorage;

    public GameObject tvScreen; // Mapped to the TV Screen In-Game

    // Start is called before the first frame update
    void Start()
    {
        simuControl = masterControl.GetComponent<SimulationController>();
        lecturer = GetComponent<AudioSource>();
        focusRender = focus.GetComponent<Renderer>();
        orbitRender = orbitTrail.GetComponent<Renderer>();

        if(tvScreen != null) // Start with TV screen off
            {tvScreen.SetActive(false);}
    }

    // Update is called once per frame
    void Update()
    {

    }
   
    void OnCollisionEnter(Collision other)
    {
        //string myname = other.gameObject.name;
        //Debug.Log("I was touched by " + myname + "with tag " + other.gameObject.tag);

        if (other.gameObject.name.Contains("Hand") && lectureOn == false)
        {
            //speedStorage = simuControl.simulationSpeed;
            //playerPositionStorage = player.transform.position;
            //podiumPositionStorage = transform.parent.transform.position;
            //podiumRotationStorage = transform.parent.transform.rotation;

            simuControl.simulationSpeed = 30f;
            //player.transform.position = new Vector3(20f, 55f, -60f);
            //transform.parent.transform.position = new Vector3(19.3f, 55.5f, -59f);

            lecturer.Play();
            tvScreen.SetActive(true); //Turn On TV when button is pressed
            focusRender.enabled = true;
            orbitRender.enabled = true;
            lectureOn = true;
        }

        else if (other.gameObject.name.Contains("Hand") && lectureOn == true)
        {
            simuControl.simulationSpeed = speedStorage;
            //player.transform.position = playerPositionStorage;
            //transform.parent.transform.position = podiumPositionStorage;
            focusRender.enabled = false;

            lecturer.Stop();
            tvScreen.SetActive(false); //Turn off TV when button is pressed again
            focusRender.enabled = false;
            orbitRender.enabled = false;
            lectureOn = false;

        }

    }

}
