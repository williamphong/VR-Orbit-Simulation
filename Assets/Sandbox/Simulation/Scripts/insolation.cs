using System;

/* Dr. T. S. Kostadinov, May 2007 - January 2013 */

/********** References/Works consulted **************/
/****************************************************/

namespace Insolation{
    public class InsolationCalculator
    {
        /// <summary>
        /// Calculates insolation averaged over 24 hrs centered on 
        /// the local solar noon, given the inputs as described below
        /// </summary>
        /// <param name="Fo">solar "constant" (the total solar irradiance at 1 AU at the TOA), in W/m^2  (but can be any unit)</param>
        /// <param name="r">length of radius vector of Earth in the same units as those of AU, e.g., millions of km</param>
        /// <param name="phi">geographic latitude on Earth, in degrees</param>
        /// <param name="delta">solar declination in degrees</param>
        /// <param name="AU">the length of 1 AU (e.g., in millions of km), needs to be in the same units as r</param>
        /// <param name="F_bar">insolation at the top of the atmosphere (TOA), averaged over 24 hrs centered on local solar noon
        /// It will be given in the units of Fo, usually W/m^2</param>
        /// <param name="daylength">length of day (meaning the duration of daylight hours) in hours</param>
        public static void Insolation(double Fo, double r, double phi, double delta, double AU, out double F_bar, out double daylength)
        {
            // deg to rad
            delta = delta * (Math.PI / 180);
            phi = phi * (Math.PI / 180);

            // Check if we are in the special regions where the Sun never rises
            // or sets and deal with these special cases in a different way.
            // Compute daylength

            bool flag = false;

            if (delta >= 0)
            {
                if (phi <= delta - Math.PI / 2)
                {
                    daylength = 0;
                    F_bar = 0;
                    flag = false;
                }
                else if (phi >= Math.PI / 2 - delta)
                {
                    daylength = 24;
                    flag = true;
                }
                else
                {
                    flag = true;
                    daylength = (24 / Math.PI) * Math.Acos(-Math.Tan(phi) * Math.Tan(delta));
                    double t_sunset = Math.Acos(-Math.Tan(delta) * Math.Tan(phi));
                    double[] t = TimeInterval(t_sunset); // this is time interval on O(10s) integration interval dt
                    double For = Fo * Math.Pow((AU / r), 2);
                    double sinh_ave = ComputeAverageSinH(delta, phi, t);
                    F_bar = For * sinh_ave;
                    F_bar *= daylength / 24;
                    return;
                }
            }
            else
            {
                if (phi >= delta + Math.PI / 2)
                {
                    daylength = 0;
                    F_bar = 0;
                    flag = false;
                }
                else if (phi <= -Math.PI / 2 - delta)
                {
                    daylength = 24;
                    flag = true;
                }
                else
                {
                    flag = true;
                    daylength = (24 / Math.PI) * Math.Acos(-Math.Tan(phi) * Math.Tan(delta));
                    double t_sunset = Math.Acos(-Math.Tan(delta) * Math.Tan(phi));
                    double[] t = TimeInterval(t_sunset);
                    double For = Fo * Math.Pow((AU / r), 2);
                    double sinh_ave = ComputeAverageSinH(delta, phi, t);
                    F_bar = For * sinh_ave;
                    F_bar *= daylength / 24;
                    return;
                }
            }

            F_bar = 0;
            daylength = 0;
        }

        private static double[] TimeInterval(double t_sunset)
        {
            int steps = 1000;
            double[] arr = new double[steps + 1];
            double dt = t_sunset / steps;
            for (int i = 0; i <= steps; i++)
            {
                arr[i] = i * dt;
            }
            return arr;
        }

        private static double ComputeAverageSinH(double delta, double phi, double[] t)
        {
            double sum = 0;
            for (int i = 0; i < t.Length; i++)
            {
                sum += Math.Sin(delta) * Math.Sin(phi) + Math.Cos(delta) * Math.Cos(phi) * Math.Cos(t[i]);
            }
            return sum / t.Length;
        }
    }
}