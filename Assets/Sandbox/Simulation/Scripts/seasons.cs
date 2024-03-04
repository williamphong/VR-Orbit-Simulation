using System;
using Keplerian;

namespace Seasons{
    /// <summary>
    /// Class <c>SeasonsCalc</c> calculates the season lengths
    /// </summary>
    public class SeasonsCalc{
        /// <summary>
        /// Computation of the length of each seasons from a time period, eccentricity, and precession
        /// </summary>
        /// <param name="T">Sidereal year length in days.</param>
        /// <param name="eccen">eccentricity of Earth's orbit</param>
        /// <param name="precession">Precession angle, in degrees</param>
        /// <param name="winter_len">Length of winter in days</param>
        /// <param name="spring_len">Length of spring in days</param>
        /// <param name="summer_len">Length of summer in days</param>
        /// <param name="fall_len">Length of fall in days</param>

        //private double winter_len, spring_len, summer_len, fall_len

        public static void CalculateSeasonLengths(double T, double eccen, double precession, out double winter_len, out double spring_len, out double summer_len, out double fall_len)
        {

            /*
            Strict & non-strict inequalities work below b/c keplerian returns T when
            true anomaly is 360 degrees (as opposed to returning 0 for time of flight)
            Logic here will break down if this functionality of keplerian is not such. 
            */
            winter_len = spring_len = summer_len = fall_len = 0.0;
            precession *= 180/Math.PI;
            //Console.WriteLine("precession" + precession);

            if (precession <= 90)
            {
                double t1, t2, t3, t4, ma;
                KeplerianSolver.keplerian(T, eccen, precession, out t1, out ma);
                KeplerianSolver.keplerian(T, eccen, 90 + precession, out t2, out ma);
                KeplerianSolver.keplerian(T, eccen, 180 + precession, out t3, out ma);
                KeplerianSolver.keplerian(T, eccen, 270 + precession, out t4, out ma);

                winter_len = t1 + (T - t4);
                spring_len = t2 - t1;
                summer_len = t3 - spring_len - t1;
                fall_len = t4 - summer_len - spring_len - t1;
                // check for consistency
                // err_len = spring_len + summer_len + fall_len + winter_len - T;
            }
            else if (precession > 90 && precession <= 180)
            {
                double t1, t2, t3, t4, ma;
                KeplerianSolver.keplerian(T, eccen, precession - 90, out t1, out ma);
                KeplerianSolver.keplerian(T, eccen, precession, out t2, out ma);
                KeplerianSolver.keplerian(T, eccen, 90 + precession, out t3, out ma);
                KeplerianSolver.keplerian(T, eccen, 180 + precession, out t4, out ma);

                fall_len = t1 + (T - t4);
                winter_len = t2 - t1;
                spring_len = t3 - winter_len - t1;
                summer_len = t4 - spring_len - winter_len - t1;
            }
            else if (precession > 180 && precession <= 270)
            {
                double t1, t2, t3, t4, ma;
                KeplerianSolver.keplerian(T, eccen, precession - 180, out t1, out ma);
                KeplerianSolver.keplerian(T, eccen, precession - 90, out t2, out ma);
                KeplerianSolver.keplerian(T, eccen, precession, out t3, out ma);
                KeplerianSolver.keplerian(T, eccen, 90 + precession, out t4, out ma);

                summer_len = t1 + (T - t4);
                fall_len = t2 - t1;
                winter_len = t3 - fall_len - t1;
                spring_len = t4 - winter_len - fall_len - t1;
            }
            else // special case of precession of exactly 360 will fall here
            {
                double t1, t2, t3, t4, ma;
                KeplerianSolver.keplerian(T, eccen, precession - 270, out t1, out ma);
                KeplerianSolver.keplerian(T, eccen, precession - 180, out t2, out ma);
                KeplerianSolver.keplerian(T, eccen, precession - 90, out t3, out ma);
                KeplerianSolver.keplerian(T, eccen, precession, out t4, out ma);

                spring_len = t1 + (T - t4);
                summer_len = t2 - t1;
                fall_len = t3 - summer_len - t1;
                winter_len = t4 - fall_len - summer_len - t1;
            }
        }
    }
}