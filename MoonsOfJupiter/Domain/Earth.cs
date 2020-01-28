using System;

namespace MoonsOfJupiter.Domain
{
    public struct Earth
    {
        private Moment Moment { get; }

        public Earth(Moment moment)
        {
            Moment = moment;
        }

        /// <summary>
        /// Mean anomaly of the Earth
        /// </summary>
        public Angle MeanAnomaly
        {
            get
            {
                double M = 357.529 + 0.9856003 * Moment.DayD;
                M = M.CorrectDegreeRange();
                return new Angle(M);
            }
        }

        /// <summary>
        /// Equation of Center
        /// </summary>
        public Angle EquationOfCenter
        {
            get
            {
                Angle M = MeanAnomaly;
                double A = 1.915 * Math.Sin(M.Radians) + 0.020 * Math.Sin(2 * M.Radians);
                return new Angle(A);
            }
        }

        /// <summary>
        /// Eccentricity of the Earth's orbit at a given moment in time
        /// </summary>
        /// <param name="m">Moment</param>
        /// <returns></returns>
        public Angle Eccentricity
        {
            get
            {
                var t = Moment.TimeT;
                double e = 0.016708634 - (0.000042037 * t) - (0.0000001267 * Math.Pow(t, 2));
                return new Angle(e);
            }
        }

        /// <summary>
        /// Radius Vector of the Earth
        /// </summary>
        public double RadiusVector
        {
            get
            {
                Angle M = MeanAnomaly;
                double R = 1.00014 - 0.01671 * Math.Cos(M.Radians) - 0.00014 * Math.Cos(2 * M.Radians);
                return R; // astronomical units (AU)
            }
        }

        /// <summary>
        /// Low Accuracy Version (22.2)
        /// </summary>
        public double ObliquityLowAccuracy
        {
            get
            {
                var t = Moment.TimeT;
                var a = new Angle(23, 26, 21.448).Degrees;
                var b = new Angle(0, 0, 46.8150).Degrees;
                var c = new Angle(0, 0, 0.00059).Degrees;
                var d = new Angle(0, 0, 0.0018130).Degrees;
                return a - (b * t) - (c * Math.Pow(t, 2)) + (d * Math.Pow(t, 3));
            }
        }

        /// <summary>
        /// High Accuracy Version (22.3)
        /// </summary>
        public double ObliquityHighAccuracy
        {
            get
            {
                double t = Moment.TimeT;
                double U = t / 100;
                return
                    new Angle(23, 26, 21.448).Degrees
                    - new Angle(0, 0, 4680.93).Degrees * U
                    - 1.55 * Math.Pow(U, 2)
                    + 1999.25 * Math.Pow(U, 3)
                    - 51.38 * Math.Pow(U, 4)
                    - 249.67 * Math.Pow(U, 5)
                    - 39.05 * Math.Pow(U, 6)
                    + 7.12 * Math.Pow(U, 7)
                    + 27.87 * Math.Pow(U, 8)
                    + 5.79 * Math.Pow(U, 9)
                    + 2.45 * Math.Pow(U, 10);
            }
        }
    }
}
