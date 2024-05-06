using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem.Sample
{
    public class PodiumController : MonoBehaviour
    {
        public GameObject masterControl;
        SimulationController simuControl;

        AudioSource lecturer;

        bool lectureOn = false;
        bool[] podiums = new bool[10];

        public float cooldown = 0f;

        float speedStorage;

        public GameObject focusModel;
        public GameObject orbitTrail;

        public GameObject keplerCamera;
        public AudioClip kepler1VO;
        public AudioClip kepler2VO;
        public AudioClip kepler3VO;
        public GameObject closeEarth;

        public GameObject earthViewCam;

        public AudioClip eccentricityVO;
        public AudioClip obliquityVO;
        public AudioClip perihelionVO;

        private void Start()
        {
            simuControl = masterControl.GetComponent<SimulationController>();
            lecturer = GetComponent<AudioSource>();

        }

        public void ToggleEarthView()
        {
            if (cooldown < Time.time)
            {
                cooldown = Time.time + 1f;
                if (lectureOn == false)
                {
                    earthViewCam.SetActive(true);
                    lectureOn = true;
                    podiums[0] = true;
                }

                else if (podiums[0] == true)
                {
                    earthViewCam.SetActive(false);
                    lectureOn = false;
                    podiums[0] = false;
                }
            }

        }


        public void ToggleKepler1()
        {
            if (cooldown < Time.time)
            {
                cooldown = Time.time + 1f;
                if (lectureOn == false)
                {
                    speedStorage = simuControl.simulationSpeed;
                    simuControl.simulationSpeed = 30f;

                    lecturer.clip = kepler1VO;
                    lecturer.Play();
                    keplerCamera.SetActive(true);
                    focusModel.SetActive(true);
                    orbitTrail.SetActive(true);
                    lectureOn = true;
                    podiums[1] = true;
                }

                else if (podiums[1] == true)
                {
                    simuControl.simulationSpeed = speedStorage;
                    focusModel.SetActive(false);

                    lecturer.Stop();
                    keplerCamera.SetActive(false);
                    orbitTrail.SetActive(false);
                    lectureOn = false;
                    podiums[1] = false;
                }
            }
        }
        public void ToggleKepler2()
        {
            if (cooldown < Time.time)
            {
                cooldown = Time.time + 1f;
                if (lectureOn == false)
                {
                    speedStorage = simuControl.simulationSpeed;
                    simuControl.simulationSpeed = 30f;

                    lecturer.clip = kepler2VO;
                    lecturer.Play();
                    keplerCamera.SetActive(true);
                    orbitTrail.SetActive(true);
                    lectureOn = true;
                    podiums[2] = true;
                }

                else if (podiums[2] == true)
                {
                    simuControl.simulationSpeed = speedStorage;

                    lecturer.Stop();
                    keplerCamera.SetActive(false);
                    orbitTrail.SetActive(false);
                    lectureOn = false;
                    podiums[2] = false;
                }
            }
        }
        public void ToggleKepler3()
        {
            if (cooldown < Time.time)
            {
                cooldown = Time.time + 1f;
                if (lectureOn == false)
                {
                    speedStorage = simuControl.simulationSpeed;
                    simuControl.simulationSpeed = 30f;

                    lecturer.clip = kepler3VO;
                    lecturer.Play();
                    closeEarth.SetActive(true);
                    keplerCamera.SetActive(true);
                    lectureOn = true;
                    podiums[3] = true;
                }

                else if (podiums[3] == true)
                {
                    simuControl.simulationSpeed = speedStorage;


                    lecturer.Stop();
                    closeEarth.SetActive(false);
                    keplerCamera.SetActive(false);
                    lectureOn = false;
                    podiums[3] = false;
                }
            }
        }

        public void DefineEccentricity()
        {
            if (cooldown < Time.time)
            {
                cooldown = Time.time + 1f;
                lecturer.clip = eccentricityVO;
                lecturer.Play();
            }
        }
        public void DefineObliquity()
        {
            if (cooldown < Time.time)
            {
                cooldown = Time.time + 1f;
                lecturer.clip = obliquityVO;
                lecturer.Play();
            }
        }
        public void DefinePerihelion()
        {
            if (cooldown < Time.time)
            {
                cooldown = Time.time + 1f;
                lecturer.clip = perihelionVO;
                lecturer.Play();
            }
        }
    }
}