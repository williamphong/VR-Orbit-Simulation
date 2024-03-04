using System;

/*
Dr. T. S. Kostadinov, Nov. 7 , 2006 - January 2013

References/Works consulted:
Meeus, J. (1998), Astronomical Algorithms, Willmann-Bell, Richmond, VA. (2009 ed.)
http://scienceworld.wolfram.com/physics/EccentricAnomaly.html
http://en.wikipedia.org/wiki/Kepler%27s_laws_of_planetary_motion
http://en.wikipedia.org/wiki/Mean_anomaly
http://en.wikipedia.org/wiki/Eccentric_anomaly

*/

namespace Keplerian{

    /// <summary>
    /// Class <c> KeplerianSolver </c> computes the forward and inverse Kepler problem
    /// </summary>
    class KeplerianSolver
    {
        /// <summary>
        /// Solves the forward Kepler problem, i.eccen. given true anomaly, returns time of flight
        /// </summary>
        /// <param name="T">Orbital period in arbitrary time units</param>
        /// <param name="eccen">Orbital eccentricity</param>
        /// <param name="true_anomaly">True anomaly in degrees, measured counterclockwise from periapsis</param>
        /// <param name="t">Period of time elapsed since the last periapsis passage (perihelion for Earth). Returned in the units of T. Also known as time of flight.</param>
        /// <param name="mean_anomaly">Mean anomaly (in radians)</param>
        public static void keplerian(double T, double eccen, double true_anomaly, out double t, out double mean_anomaly)
        {
            /*
            Treat special cases first, insure that T (instead of 0) is returned if nu==360
            This ensures that orbit.m code that calculates season lengths treats the
            cases when the longitude of perihelion is an integer multiple of pi/2
            radians correctly. 
            */

            // For true anomaly >360 and negative true anomaly's, return true anomaly 
            // to remainder angle only, measured CCW from perihelion.
            
            if (true_anomaly > 360 || true_anomaly < 0)
            {
                true_anomaly = true_anomaly % 360;
            }

            if (true_anomaly == 0)
            {
                t = 0;
                mean_anomaly = 0;
            }
            else if (true_anomaly == 180)
            {
                t = T / 2;
                mean_anomaly = Math.PI;
            }
            else if (true_anomaly == 360)
            {
                t = T;
                mean_anomaly = 2 * Math.PI;
            }
            else
            {
                double E = 2 * Math.Atan(Math.Tan(true_anomaly / 2 * (Math.PI / 180)) * Math.Sqrt((1 - eccen) / (1 + eccen)));

                mean_anomaly = E - eccen * Math.Sin(E); // Kepler's Equation, eccen.g. Meeus 30.5

                t = (mean_anomaly * T) / (2 * Math.PI);

                if (mean_anomaly < 0) // t < 0 as well, orbit past aphelion
                {
                    mean_anomaly += 2 * Math.PI;
                    t = T + t;
                }
            }
        }


        /*
        From Meeus Ch. 30, page 206 - Third method.
        Due to Roger Sinnott, Sky & Telescope, 1985
        */

        /// <summary>
        /// Solves the inverse Kepler problem, i.eccen. given time of flight since last periapsis,
        /// returns true anomaly
        /// </summary>
        /// <param name="eccen">orbital eccentricity</param>
        /// <param name="mean_anomaly">mean anomaly in degrees</param>
        /// <param name="true_anomaly">true anomaly in degrees</param>
        /// <returns></returns>
        public static double keplerian_inverse(double eccen, double mean_anomaly, out double true_anomaly) {

            mean_anomaly = mean_anomaly * (Math.PI / 180);
            int N = 55; // Number of steps, this will ensure ~16 digits of accuracy, should be very sufficient
                        // (Meeus recommends 53 for 16-digit precision machine)

            // SINNOTT code (as published by MEEUS), translated from BASIC by T.S. Kostadinov:
            double F = Math.Sign(mean_anomaly);
            mean_anomaly = Math.Abs(mean_anomaly) / (2 * Math.PI);
            mean_anomaly = (mean_anomaly - Math.Floor(mean_anomaly)) * 2 * Math.PI * F;

            if (mean_anomaly < 0) 
            {
                mean_anomaly = mean_anomaly + 2 * Math.PI;
            }

            F = 1;

            if (mean_anomaly > Math.PI) 
            {
                F = -1;
                mean_anomaly = 2 * Math.PI - mean_anomaly;
            }

            double Eo = Math.PI / 2;
            double D = Math.PI / 4;

            for (int j = 1; j <= N; j++) 
            {
                double M1 = Eo - eccen * Math.Sin(Eo);
                Eo = Eo + D * ((mean_anomaly - M1) >= 0 ? 1 : -1);
                D = D / 2;
            }
            
            Eo = Eo * F;

            // END OF SINNOTT CODE

            true_anomaly = 2 * Math.Atan(Math.Tan(Eo / 2) * Math.Sqrt((1 + eccen) / (1 - eccen))) * (180 / Math.PI); // Meeus Eq. 30.1

            if (true_anomaly < 0)
            {
                true_anomaly += 360;
            }


            return true_anomaly;
        }

    }
}