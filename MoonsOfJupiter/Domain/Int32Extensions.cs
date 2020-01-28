using System;

namespace MoonsOfJupiter.Domain
{
    public static class Int32Extensions
    {
        /// <summary>
        /// Approximation of Terrestrial Date (TD) ΔT correction to UTC
        /// comes from NASA formula for years between 2005 and 2050 only:
        /// https://eclipse.gsfc.nasa.gov/SEhelp/deltatpoly2004.html
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static double ApproximateDeltaT(this int year)
        {
            var t = year - 2000;
            return 62.92 + 0.32217 * t + 0.005589 * Math.Pow(t, 2);
        }
    }
}
