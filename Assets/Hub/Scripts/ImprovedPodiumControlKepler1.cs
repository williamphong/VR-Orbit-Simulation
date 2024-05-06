using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Lecture1Button : MonoBehaviour
    {
        public GameObject masterControl;
        SimulationController simuControl;

        AudioSource lecturer;

        public GameObject focusModel;
        Renderer focusRender;

        public GameObject orbitTrail;
        Renderer orbitRender;

        bool lectureOn = false;
        float speedStorage;

        public GameObject tvScreen; // Mapped to the TV Screen In-Game

        private void Start()
        {
            simuControl = masterControl.GetComponent<SimulationController>();
            lecturer = GetComponent<AudioSource>();
            focusRender = focusModel.GetComponent<Renderer>();
            orbitRender = orbitTrail.GetComponent<Renderer>();

            if (tvScreen != null) // Start with TV screen off
            { tvScreen.SetActive(false); }

        }

        public void ToggleLecture()
        {
            if (lectureOn == false)
            {
                speedStorage = simuControl.simulationSpeed;
                simuControl.simulationSpeed = 30f;

                lecturer.Play();
                tvScreen.SetActive(true); //Turn On TV when button is pressed
                focusRender.enabled = true;
                orbitRender.enabled = true;
                lectureOn = true;
            }

            else if (lectureOn == true)
            {
                simuControl.simulationSpeed = speedStorage;
                focusRender.enabled = false;

                lecturer.Stop();
                tvScreen.SetActive(false); //Turn off TV when button is pressed again
                focusRender.enabled = false;
                orbitRender.enabled = false;
                lectureOn = false;

            }
        }
    }
}