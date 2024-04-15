
using System;
using Bergers;
using Insolation;
using Keplerian;
using Seasons;
using System.Numerics;
using GenerateRotation;

namespace GenOrbit{
public class GenOrbit
{

    /// <summary>
    /// Calculates insolation averaged over 24 hrs centered on 
    /// the local solar noon, given the inputs as described below
    /// </summary>
    /// <param name="r_vec_length">length of the radius-vector of Earth, in the units of a </param>
    /// <param name="sun_decl">geocentric declination of the Sun in degrees</param>
    /// <param name="sol">insolation at the TOA averaged over 24 hrs, in the units of Fo</param>
    /// <param name="season_length">length/duration of seasons in days</param>
    /// <param name="daylength">duration of daylight in hours</param>
    /// <param name="true_anomaly">true anomaly of Earth in degrees</param>
    public static void OrbitCalc(double a, double AU, double T, double eccen, double obliq, double precession, double mean_anomaly, double Fo, double latitude, out double r_vec_length, out double sun_decl, out double sol, out double[] season_length, out double daylength, out double true_anomaly)
        {
            // output declaration/init
            double winter_len, spring_len, summer_len, fall_len; // season day lengths
            season_length = new double[4]; // array of season day lengths

            // Inverse Keplerian to find the true anomaly 
            KeplerianSolver.keplerian_inverse(eccen, mean_anomaly, out true_anomaly);
            Console.WriteLine("\nInverse Keplerian -----------------");
            Console.WriteLine("True anomaly (in radians): " + true_anomaly + " in deg: " + true_anomaly*Math.PI/180);

            double position_e = true_anomaly * (Math.PI / 180); // for parameterizing radius-vector of Earth

            double b = a * Math.Sqrt(1 - Math.Pow(eccen, 2)); // b = semi minor axis
            double c = eccen * a; // distance from center to focus
            Console.WriteLine("\nb: " + b + " c: " + c);

            float position = (float)precession;
            float r = (float)(a * (1 - Math.Pow((float)eccen, 2)) / (1 + (float)eccen * Math.Cos(position)));
            float x = (float)(r * Math.Cos(position));
            float y = (float)(r * Math.Sin(position));
            Vector3 r_spring = new Vector3(x, y, 0);

            position = (float)precession+(float)Math.PI/2;
            r = (float)(a * (1 - Math.Pow((float)eccen, 2)) / (1 + (float)eccen * Math.Cos(position)));
            x = (float)(r * Math.Cos(position));
            y = (float)(r * Math.Sin(position));
            Vector3 r_summer = new Vector3(x, y, 0);

            position = (float)precession+(float)Math.PI; 
            r = (float)(a * (1 - Math.Pow((float)eccen, 2)) / (1 + (float)eccen * Math.Cos(position)));
            x = (float)(r * Math.Cos(position));
            y = (float)(r * Math.Sin(position));
            Vector3 r_fall = new Vector3(x, y, 0);

            position = (float)precession+3*(float)Math.PI/2;
            r = (float)(a * (1 - Math.Pow((float)eccen, 2)) / (1 + (float)eccen * Math.Cos(position)));
            x = (float)(r * Math.Cos(position));
            y = (float)(r * Math.Sin(position));
            Vector3 r_winter = new Vector3(x, y, 0);

            //Radius vector of the Earth 
            r = (float)(a * (1 - Math.Pow((float)eccen, 2)) / (1 + (float)eccen * Math.Cos((float)position_e))); //center of coordinate system for this parametrization is the Sun, in the right focus of the ellipse.
            x = (float)(r * Math.Cos(position_e));
            y = (float)(r * Math.Sin(position_e));
            Vector3 r_vector =  new Vector3(x, y, 0);
            r_vec_length = r_vector.Length();


            double radius = a*(1 - Math.Pow(eccen, 2)) / (1 + eccen * Math.Cos(position_e));
            Console.WriteLine("\nr: " + r + " r vec: " + r_vector);

            /*
            Console.WriteLine("\nr_spring vector: " + r_spring);
            Console.WriteLine("r_summer vector: " + r_summer);
            Console.WriteLine("r_fall vector: " + r_fall);
            Console.WriteLine("r_winter vector: " + r_winter);
            Console.WriteLine("r vector: " + r_vector);
            Console.WriteLine("r vector mag: " + r_vector.Length());
            */

            // Compute a rotation matrix about a vector in the xy plane (plane of the ecplitic), 
            // whose orientation is determined by the precession angle; 
            // angle of rotation around that vector's direction will be equal to the obliquity. 
            // That way the Earth's equatorial plane ends up at an angle equal to the obliquity 
            // with respect to the ecliptic plane.
            double[,] tilt_m;
            GenerateRot.rotation(obliq, precession, out tilt_m);

            float R = (float)0.15*(float)a; //this determines the size of Earth for plotting purposes, relative to the orbit size
            float R_sun = 1/3*R; //size of the Sun for the plot

            Vector3 k = new Vector3(0, 0, R * 1.5f);

            float x1 = (float)tilt_m[0, 0] * k.X + (float)tilt_m[0, 1] * k.Y + (float)tilt_m[0, 2] * k.Z;
            float y1 = (float)tilt_m[1, 0] * k.X + (float)tilt_m[1, 1] * k.Y + (float)tilt_m[1, 2] * k.Z;
            float z1 = (float)tilt_m[2, 0] * k.X + (float)tilt_m[2, 1] * k.Y + (float)tilt_m[2, 2] * k.Z;
            Vector3 kk = new Vector3(x1, y1, z1);

            sun_decl = (180/Math.PI) * Math.Acos(Vector3.Dot(r_vector, kk) / (kk.Length() * r_vector.Length())) - 90;

            InsolationCalculator.Insolation(Fo, r_vector.Length(), latitude, -6.380, AU, out sol, out daylength);


            // Output the rotation matrix (if needed)
            
            /*
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine("tilt_m[" + i + "," + j + "]: " + tilt_m[i, j]);
                }
            }      
            */
            

            SeasonsCalc.CalculateSeasonLengths(T, eccen, precession, out winter_len, out spring_len, out summer_len, out fall_len);
            season_length[0] = spring_len;
            season_length[1] = summer_len;
            season_length[2] = fall_len;
            season_length[3] = winter_len;
            
        }
    }
}
