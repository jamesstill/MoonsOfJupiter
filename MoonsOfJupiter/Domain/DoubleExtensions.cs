﻿using System;

namespace MoonsOfJupiter.Domain
{
    public static class DoubleExtensions
    {
        public static double ToRadians(this double d)
        {
            return d * (Math.PI / 180); // convert degrees to radians
        }

        public static double ToDegrees(this double d)
        {
            return d * (180 / Math.PI); // convert radians to degrees
        }

        /// <summary>
        /// For very large angles correct to put between 0 and 360 degrees
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double CorrectDegreeRange(this double d)
        {
            d %= 360;
            if (d < 0)
            {
                d += 360;
            }

            return d;
        }

        /// <summary>
        /// Given a value 123.45678 returns 0.45678
        /// </summary>
        /// <param name="d"></param>
        /// <returns>integral of a floating point number</returns>
        public static double GetIntegral(this double d)
        {
            return d - Math.Truncate(d);
        }

        /// <summary>
        /// Given a value 123.45678 returns 123
        /// </summary>
        /// <param name="d"></param>
        /// <returns>integer part of </returns>
        public static int GetIntegerPart(this double d)
        {
            return (int)Math.Truncate(d);
        }

        /// <summary>
        /// Given a value 123.45678 returns 45678 (no leading zero and decimal)
        /// </summary>
        /// <param name="d"></param>
        /// /// <returns>fractional part of floating point number</returns>
        public static double GetFractionalPart(this double d)
        {
            return d - Math.Truncate(d);
        }

        public static string ToFractionalPart(this double d, int precision)
        {
            string format = string.Empty.PadRight(precision, '0');
            int f = (int)((d % 1) * Math.Pow(10, precision));
            return f.ToString(format);
        }
    }
}
