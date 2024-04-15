using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bergers;
using Insolation;
using Keplerian;
using Seasons;
using System.Numerics;
using GenerateRotation;
using TimeConversion;
using TMPro;

public class OverlayFeed : MonoBehaviour
{
        double AU = 149.597870700; // in millions of km, 2013 Selected Astronomical Constants, from the online Astronomical Almanac at http://asa.usno.navy.mil/, section K
        double sm_axis = 1.00000261*AU; // (Standish, E. Myles; Williams, James C.. "Orbital Ephemerides of the Sun, Moon, and Planets" (PDF). International Astronomical Union Commission 4: (Ephemerides).
        double eccentricity = 0.01670236225492288; // Laskar 2004 solution for J2000.0 (year 0  for him)
        double obliquity = 0.4090928042223415; //radians, %Laskar 2004 solution for J2000.0 (year 0  for him)
        double precession = Math.PI - 1.796256991128036; //radians, Laskar 2004 solution for J2000.0 (year 0  for him); complementary angle calculated here for internal use only
        double mean_anomaly = 90; //in degrees, CCW from perihelion, tied to time passage and via Kpler's II lawto true anomaly
        double Fo = 1366; // total solar irradance at 1 AU
        double latitude = 43; // latitude on Earth
        double T = 365.256363; // Sidereal year length in days. (Should equinox-to-equinox year be used?)
                            //T is prescribed a-priori, as Kepler's III Law is not in the model
                            // period

        // VARS
        double daylength; // day length
        double r_vec_length, sun_decl, sol;
        double[] season_length; // season day lengths in order of winter, spring, summer, fall

        int year = 2000; // Example year - user can change
        double longitudePerihelion; // longitude of perihelion
        double longitudePerigee; // longitude of perigee
        double true_anomaly; // true anomaly
        double time_elapsed; // time elapsed
        double prec_for_orbit; // precession for orbit
        double true_solar; // true solar longitude, in degrees 
        double mean_solar; // mean solar longitude, in degrees
        double mean_anomaly_equinox; // mean anomaly 
        double time_since_perihelion; // time since perihelion


    private string textToDisplay = "";
    public TMP_Text overlay;

    public GameObject masterControl;
    SimulationController simuControl;

    void Start()
    {
        simuControl = masterControl.GetComponent<SimulationController>();
    }

    void Update()
    {
        textToDisplay = "";

        textToDisplay += "Current Date: " + simuControl.dateRead();

        year = simuControl.getYear();

        int month = simuControl.getMonth();

        int day = simuControl.getDay() + simuControl.getHours() + simuControl.getSecond();
        
        int ndays;
        
        switch(month){
            case 1:
                ndays = 0;
                break;
            case 2:
                ndays = 31;
                break;
            case 3:
                ndays = 59;
                break;
            case 4:
                ndays = 90;
                break;
            case 5:
                ndays = 120;
                break;
            case 6:
                ndays = 151;
                break;
            case 7:
                ndays = 181;
                break;
            case 8:
                ndays = 212;
                break;
            case 9:
                ndays = 243;
                break;
            case 10;
                ndays = 273;
                break;
            case 11:
                ndays = 304;
                break;
            case 12:
                ndays = 334;
                break;
            default:
            //error
        }

        int dayofyear = ndays + (day - 1);

        BergerSol.CalculateOrbitalParameters(year, out eccentricity, out obliquity, out longitudePerihelion);

        double omega_bar = longitudePerihelion - 180;
        omega_bar = omega_bar < 0 ? omega_bar + 360 : omega_bar;
        //Console.WriteLine("omega_bar " + omega_bar);

        int dayofequinox = 31+28+19;

        int days_since_spring = dayofyear - dayofequinox;
        days_since_spring = days_since_spring < 0 ? days_since_spring + 365 : days_since_spring;

         if(prec_for_orbit<0){
            prec_for_orbit += 360;
        }
        //Console.WriteLine("prec for orbit: " + prec_for_orbit);

        // determine mean anomaly of spring equinox - this will be March 20 always and will depend on precession's value;
        KeplerianSolver.keplerian(T, eccentricity, prec_for_orbit, out time_since_perihelion, out mean_anomaly_equinox);
        
        time_elapsed = (days_since_spring + time_since_perihelion) % T;

        mean_anomaly = 360*(time_elapsed/T);

        GenOrbit.OrbitCalc(sm_axis, AU, T, eccentricity, obliquity, prec_for_orbit, mean_anomaly, Fo, latitude, out r_vec_length, out sun_decl, out sol, out season_length, out daylength, out true_anomaly);
        
        true_solar = (true_anomaly + omega_bar)%360;

        textToDisplay += "\nBerger's Solution --------------------";
        textToDisplay += "\nEccentricity (dimensionless): " + eccentricity;
        textToDisplay += "\nObliquity (degrees): " + obliquity;
        textToDisplay += "\nLongitude of Perihelion (degrees): " + longitudePerihelion;

        textToDisplay += "\n\nKeplerian -----------------";
        textToDisplay += "\nPeriod of time elapsed since last periapsis passage: " + t;
        textToDisplay += "\nTrue anomaly (in radians): " + true_anomaly + " in deg: " + true_anomaly*Mathf.PI/180;
        textToDisplay += "\nMean anomaly (in radians): " + mean_anomaly + " in deg: " + mean_anomaly*Mathf.PI/180;
        
        textToDisplay += "\n\nNorthern Hemisphere Seasons Lengths (days) -----------------";
        textToDisplay += "\nNorthern Hemisphere Seasons Lengths (days)"
                                + "\n   Spring Length: " + season_length[0]
                                + "\n   Summer Length: " + season_length[1]
                                + "\n   Fall Length: " + season_length[2]
                                + "\n   Winter Length: " + season_length[3];

        textToDisplay += "\n\nLongitude of perigee (degrees): " + omega_bar
                                + "\n   True longitude (degrees): " + true_solar
                                + "\n   Mean longitude (degrees): " + mean_solar;

        textToDisplay += "\n\nTrue solar longitude: " + (true_anomaly + longitudePerihelion) % 360;

        overlay.text = textToDisplay;


    }
    
}