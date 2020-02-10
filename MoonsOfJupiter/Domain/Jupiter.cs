using System;

namespace MoonsOfJupiter.Domain
{
    public struct Jupiter
    {
        private Moment Moment { get; }

        public Jupiter(Moment moment)
        {
            Moment = moment;
        }

        /// <summary>
        /// Ephemeris from Chapter 43, p. 293
        /// </summary>
        public double T1 { 
            get
            {
                double d = Moment.JDE - 2433282.5;
                return d / 36525;
            } 
        }

        /// <summary>
        /// Long-period term in the motion of Jupiter
        /// </summary>
        public Angle LongPeriodTerm
        {
            get
            {
                double V = 172.74 + 0.00111588 * Moment.DayD;
                return new Angle(V);
            }
        }

        /// <summary>
        /// Mean anomaly of Jupiter
        /// </summary>
        public Angle MeanAnomaly
        {
            get
            {
                double N = 20.020 + 0.0830853 * Moment.DayD + 0.329 * Math.Sin(LongPeriodTerm.Radians);
                return new Angle(N);
            }
        }

        /// <summary>
        /// Equation of Center
        /// </summary>
        public Angle EquationOfCenter
        {
            get
            {
                Angle N = MeanAnomaly;
                double B = 5.555 * Math.Sin(N.Radians) + 0.168 * Math.Sin(2 * N.Radians);
                return new Angle(B);
            }
        }

        public Angle DeltaHeliocentricLongitudeEarthAndJupiter
        {
            get
            {
                double J = 66.115 + 0.9025179 * Moment.DayD - 0.329 * Math.Sin(LongPeriodTerm.Radians);
                J = J.CorrectDegreeRange();
                return new Angle(J);
            }
        }

        /// <summary>
        /// Radius Vector of Jupiter
        /// </summary>
        public double RadiusVector
        {
            get
            {
                Angle N = MeanAnomaly;
                double r = 5.20872 - 0.25208 * Math.Cos(N.Radians) - 0.00611 * Math.Cos(2 * N.Radians);
                return r; // astronomical units (AU)
            }
        }
    }
}
