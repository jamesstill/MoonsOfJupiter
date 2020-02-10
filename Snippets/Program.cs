using System;

namespace Snippets
{
    class Program
    {
        static void Main(string[] args)
        {
            var year = 2020;
            var t = (year - 2000) / 100;
            var deltaT = 102 + (102 * t) + (25.3 * Math.Pow(t, 2));
            var correction = 0.37 * (year - 2100);
            var correctedDeltaT = deltaT + correction;

            Console.WriteLine("Corrected DT for the year {0} is: {1}", 
                year, 
                correctedDeltaT);

            var approxDeltaT = ApproximateDeltaT(year);
            Console.WriteLine("Approximate DeltaT: {0}", approxDeltaT);
        }

        /// <summary>
        /// Approximation of Terrestrial Date (TD) ΔT correction to UTC
        /// comes from NASA formula for years between 2005 and 2050 only:
        /// https://eclipse.gsfc.nasa.gov/SEhelp/deltatpoly2004.html
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static double ApproximateDeltaT(int year)
        {
            var t = year - 2000;
            return 62.92 + 0.32217 * t + 0.005589 * t * t;
        }
    }
}
