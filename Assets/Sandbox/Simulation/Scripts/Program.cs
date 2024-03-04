using System;
using Bergers;
using Insolation;
using Keplerian;
using Seasons;
using System.Numerics;
using GenerateRotation;

public class Program
{
public static void Main(string[] args)
    {
        // default constants 
        double AU = 149.597870700; // in millions of km, 2013 Selected Astronomical Constants, from the online Astronomical Almanac at http://asa.usno.navy.mil/, section K
        double a = 1.00000261*AU; // (Standish, E. Myles; Williams, James C.. "Orbital Ephemerides of the Sun, Moon, and Planets" (PDF). International Astronomical Union Commission 4: (Ephemerides).
        double eccentricity = 0.01670236225492288; // Laskar 2004 solution for J2000.0 (year 0  for him)
        double obliquity = 0.4090928042223415; //radians, %Laskar 2004 solution for J2000.0 (year 0  for him)
        double precession = Math.PI - 1.796256991128036; //radians, Laskar 2004 solution for J2000.0 (year 0  for him); complementary angle calculated here for internal use only
        double mean_anomaly = 90; //in degrees, CCW from perihelion, tied to time passage and via Kpler's II lawto true anomaly
        double Fo = 1366;
        double latitude = 43;
        double T = 365.256363; // Sidereal year length in days. (Should equinox-to-equinox year be used?)
                        //T is prescribed a-priori, as Kepler's III Law is not in the model
        
        int year = 2000; // Example year - user can change
        double longitudePerihelion; // longitude of perihelion
        double true_anomaly; // true anomaly
        double t; // time
        double winter_len, spring_len, summer_len, fall_len; // season day lengths
        double F_bar; // insolation
        double daylength; // day length

        BergerSol.CalculateOrbitalParameters(year, out eccentricity, out obliquity, out longitudePerihelion);
        Console.WriteLine("Berger's Solution --------------------");
        Console.WriteLine("Year: " + year);
        Console.WriteLine("Eccentricity (dimensionless): " + eccentricity);
        Console.WriteLine("Obliquity (degrees): " + obliquity);
        Console.WriteLine("Longitude of Perihelion (degrees): " + longitudePerihelion);

        KeplerianSolver.keplerian_inverse(eccentricity, mean_anomaly, out true_anomaly);
        Console.WriteLine("\nInverse Keplerian -----------------");
        Console.WriteLine("True anomaly (in radians): " + true_anomaly + " in deg: " + true_anomaly*Math.PI/180);

        KeplerianSolver.keplerian(T, eccentricity, true_anomaly, out t, out mean_anomaly);
        Console.WriteLine("\nKeplerian -----------------");
        Console.WriteLine("Period of time elapsed since last periapsis passage: " + t);
        Console.WriteLine("Mean anomaly (in radians): " + mean_anomaly + " in deg: " + mean_anomaly*Math.PI/180);

        SeasonsCalc.CalculateSeasonLengths(T, eccentricity, precession, out winter_len, out spring_len, out summer_len, out fall_len);

        Console.WriteLine("\nNorthern Hemisphere Seasons Lengths (days) -----------------");
        Console.WriteLine("Spring Length: " + spring_len);
        Console.WriteLine("Summer Length: " + summer_len);
        Console.WriteLine("Fall Length: " + fall_len);
        Console.WriteLine("Winter Length: " + winter_len);

        Console.WriteLine("\nTrue solar longitude: " + (true_anomaly + longitudePerihelion) % 360);


        double position_e = true_anomaly * (Math.PI / 180); // for parameterizing radius-vector of Earth

        double b = a * Math.Sqrt(1 - Math.Pow(eccentricity, 2)); // b = semi minor axis
        double c = eccentricity * a; // distance from center to focus

        float position = (float)precession;
        float r = (float)(a * (1 - Math.Pow((float)eccentricity, 2)) / (1 + (float)eccentricity * Math.Cos(position)));
        float x = (float)(r * Math.Cos(position));
        float y = (float)(r * Math.Sin(position));
        Vector3 r_spring = new Vector3(x, y, 0);

        position = (float)precession+(float)Math.PI/2;
        r = (float)(a * (1 - Math.Pow((float)eccentricity, 2)) / (1 + (float)eccentricity * Math.Cos(position)));
        x = (float)(r * Math.Cos(position));
        y = (float)(r * Math.Sin(position));
        Vector3 r_summer = new Vector3(x, y, 0);

        position = (float)precession+(float)Math.PI; 
        r = (float)(a * (1 - Math.Pow((float)eccentricity, 2)) / (1 + (float)eccentricity * Math.Cos(position)));
        x = (float)(r * Math.Cos(position));
        y = (float)(r * Math.Sin(position));
        Vector3 r_fall = new Vector3(x, y, 0);

        position = (float)precession+3*(float)Math.PI/2;
        r = (float)(a * (1 - Math.Pow((float)eccentricity, 2)) / (1 + (float)eccentricity * Math.Cos(position)));
        x = (float)(r * Math.Cos(position));
        y = (float)(r * Math.Sin(position));
        Vector3 r_winter = new Vector3(x, y, 0);

        //Radius vector of the Earth 
        r = (float)(a * (1 - Math.Pow((float)eccentricity, 2)) / (1 + (float)eccentricity * Math.Cos((float)position_e))); //center of coordinate system for this parametrization is the Sun, in the right focus of the ellipse.
        x = (float)(r * Math.Cos(position_e));
        y = (float)(r * Math.Sin(position_e));
        Vector3 r_vector =  new Vector3(x, y, 0);


        double radius = a*(1 - Math.Pow(eccentricity, 2)) / (1 + eccentricity * Math.Cos(position_e));
        
        
        InsolationCalculator.Insolation(Fo, r_vector.Length(), latitude, -8.285, AU, out F_bar, out daylength);
        
        Console.WriteLine("\nInsolation -----------------");
        Console.WriteLine("Insolation (TOA): " + F_bar);
        Console.WriteLine("Day length: " + daylength);
        Console.WriteLine("Sun-Earth distance in km*1e+6: " + r);


        Console.WriteLine("\nr_spring vector: " + r_spring);
        Console.WriteLine("r_summer vector: " + r_summer);
        Console.WriteLine("r_fall vector: " + r_fall);
        Console.WriteLine("r_winter vector: " + r_winter);
        Console.WriteLine("r vector: " + r_vector);

        double[,] tilt_m;
        GenerateRot.rotation(obliquity, precession, out tilt_m);

        float R = (float)0.15*(float)a; //this determines the size of Earth for plotting purposes, relative to the orbit size
        float R_sun = 1/3*R; //size of the Sun for the plot

        Vector3 k = new Vector3(0, 0, R * 1.5f);

        float x1 = (float)tilt_m[0, 0] * k.X + (float)tilt_m[0, 1] * k.Y + (float)tilt_m[0, 2] * k.Z;
        float y1 = (float)tilt_m[1, 0] * k.X + (float)tilt_m[1, 1] * k.Y + (float)tilt_m[1, 2] * k.Z;
        float z1 = (float)tilt_m[2, 0] * k.X + (float)tilt_m[2, 1] * k.Y + (float)tilt_m[2, 2] * k.Z;
        Vector3 kk = new Vector3(x1, y1, z1);

        double solar_declination = (180/Math.PI) * Math.Acos(Vector3.Dot(r_vector, kk) / (kk.Length()*r_vector.Length())) - 90;
        Console.WriteLine("\nSolar declination: " + solar_declination);

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
        
    }

    public static void orbit(){
        
    }
}