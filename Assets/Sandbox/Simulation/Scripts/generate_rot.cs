using System;

namespace GenerateRotation{
    /*
    Construction of rotation matrix to rotate Earth around dir_vector by
    obliquity degrees. This is an affine transformation.

    The code below uses ideas from Wikipedia's page on Rotation matrix:
    http://en.wikipedia.org/wiki/Rotation_matrix

    dir_vector = [cos(precession) sin(precession) 0 ];
    u,v,w components directly coded below
    Axis of rotation is in the orbital plane (w-component = 0), Earth to be
    rotated by an angle equal to obliquity

    This is a utility function expected to be called only internally by the
    orbit model, and no input checking/warnings are performed.

    Dr. T.Kostadinov
    */
    public class GenerateRot
    {
        public static void rotation(double obliquity, double precession, out double[,] tilt_m )
        {
            // Convert degrees to radians
            double obliquityRad = obliquity * (Math.PI / 180);
            //double precessionRad = precession * (Math.PI / 180);

            // Calculate trigonometric values
            double cos_eps = Math.Cos(-obliquityRad);
            double sin_eps = Math.Sin(-obliquityRad);
            double onem_coseps = 1 - cos_eps;

            // Define components of the rotation matrix
            double u = Math.Cos(precession);
            double v = Math.Sin(precession);
            double w = 0.0;

            // Construct the rotation matrix
            tilt_m = new double[3, 3];
            tilt_m[0, 0] = cos_eps + (Math.Pow(u, 2) * onem_coseps);
            tilt_m[0, 1] = (u * v * onem_coseps) - (w * sin_eps);
            tilt_m[0, 2] = (u * w * onem_coseps) + (v * sin_eps);

            tilt_m[1, 0] = (u * v * onem_coseps) + (w * sin_eps);
            tilt_m[1, 1] = cos_eps + (Math.Pow(v, 2) * onem_coseps);
            tilt_m[1, 2] = (v * w * onem_coseps) - (u * sin_eps);

            tilt_m[2, 0] = (u * w * onem_coseps) - (v * sin_eps);
            tilt_m[2, 1] = (v * w * onem_coseps) + (u * sin_eps);
            tilt_m[2, 2] = cos_eps + (Math.Pow(w, 2) * onem_coseps);
        }
    }

}