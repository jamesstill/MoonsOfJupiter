using System;

namespace MoonsOfJupiter.Domain
{
    public struct Angle
    {
        public double Radians { get; }
        public double Degrees { get; }

        /// <summary>
        /// Constructs an angle based on decimal, e.g. 28°.5793
        /// </summary>
        /// <param name="degrees"></param>
        public Angle(double degrees)
        {
            Degrees = degrees;
            Radians = (Math.PI / 180) * degrees;
        }

        /// <summary>
        /// Constructs an angle based on R.A. 13h 07m 31s
        /// </summary>
        /// <param name="hours">23</param>
        /// <param name="minutes">26</param>
        /// <param name="seconds">44.001</param>
        public Angle(double hours, double minutes, double seconds)
        {
            Degrees = hours + (minutes / 60) + (seconds / 3600);
            Radians = (Math.PI / 180) * Degrees;
        }

        /// <summary>
        /// Display in format of decimal degrees. E.g.: 30°.2040
        /// </summary>
        /// <returns></returns>
        public string ToDegrees()
        {
            double i = Degrees.GetIntegerPart();
            double f = Math.Abs(Degrees.GetFractionalPart());
            // put in format ".XXXX"
            string dd = f.ToFractionalPart(6);

            if (i == 0 && Degrees < 0) // special case
            {
                return string.Format("-{0}°.{1}", i, dd);
            }

            return string.Format("{0}°.{1}", i, dd);
        }

        /// <summary>
        /// Display in format of degrees, arcminutes, and arcseconds. E.g.: 30° 20' 10''
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            double f;
            double d = Degrees.GetIntegerPart(); // degrees

            f = Degrees.GetIntegral() * 60; // arcminutes
            double m = Math.Abs(f.GetIntegerPart());
            double i = f.GetIntegral();

            f = i.GetIntegral() * 60; // arcseconds
            double s = Math.Abs(f.GetIntegerPart());

            return string.Format("{0}° {1}' {2}''", d, m, s);
        }
    }
}
