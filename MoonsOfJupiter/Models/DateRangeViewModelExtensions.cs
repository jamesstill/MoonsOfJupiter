using MoonsOfJupiter.Domain;
using System;
using System.Collections.Generic;

namespace MoonsOfJupiter.Models
{
    public static class DateRangeViewModelExtensions
    {
        public static DateRangeViewModel CalculateDateRange(this DateRangeViewModel vm)
        {
            foreach (var date in EachDay(vm.StartDate, vm.EndDate))
            {
                MoonsOnDateViewModel moons = new MoonsOnDateViewModel
                {
                    Date = date,
                    Moons = CalculateMoonPositionsForDate(date)
                };

                vm.MoonsForDate.Add(moons);
            }
         
            return vm;
        }

        private static List<MoonViewModel> CalculateMoonPositionsForDate(DateTime dt)
        {
            // using NASA formula for Year 2020
            DateTime TD = dt.AddSeconds(dt.Year.ApproximateDeltaT());

            Moment moment = new Moment(TD.Year, TD.Month, TD.Day, TD.Hour, TD.Minute, TD.Second, TD.Millisecond);

            Earth earth = new Earth(moment);
            Jupiter jupiter = new Jupiter(moment);

            Angle V = jupiter.LongPeriodTerm;
            Angle J = jupiter.DeltaHeliocentricLongitudeEarthAndJupiter;

            Angle A = earth.EquationOfCenter;
            Angle B = jupiter.EquationOfCenter;
            Angle K = new Angle(J.Degrees + A.Degrees - B.Degrees);
            double R = earth.RadiusVector;
            double r = jupiter.RadiusVector;

            // distance from Earth to Jupiter in AU
            double delta = Math.Sqrt((r * r) + (R * R) - 2 * r * R * Math.Cos(K.Radians));

            // phase angle of Jupiter
            double psi = Math.Asin(R / delta * Math.Sin(K.Radians));
            Angle psiAngle = new Angle(psi.ToDegrees());

            double d = moment.DayD - (delta / 173);

            // Jupiter's heliocentric longitude referred to the equinox of 2000.0
            double lambda = 34.35 + 0.083091 * d + 0.329 * Math.Sin(V.Radians) + B.Degrees;
            Angle lambdaAngle = new Angle(lambda);

            // Jupiter's inclination of the equator on the orbital plane is 3.12 degrees
            double Ds = 3.12 * Math.Sin(lambdaAngle.Radians + 0.74700091985);

            // Jupiter's inclination of the equator on the ecliptic is 2.22 degrees
            double Dx = 2.22 * Math.Sin(psiAngle.Radians) * Math.Cos(lambdaAngle.Radians + 0.38397243544); // should b e -0.350

            // Jupiter's inclination of the orbital plane on the ecliptic is 1.30 degrees
            double Dy = 1.30 * ((r - delta) / delta) * Math.Sin(lambdaAngle.Radians - 1.7540558983);

            // planetocentric declination De of the Earth
            double De = Ds - Dx - Dy;

            // Chap 44 algorithms

            double p = psiAngle.Degrees;

            double u1 = 163.8069 + 203.4058646 * d + p - B.Degrees;
            double u2 = 358.4140 + 101.2916335 * d + p - B.Degrees;
            double u3 = 5.7176 + 50.2345180 * d + p - B.Degrees;
            double u4 = 224.8092 + 21.4879800 * d + p - B.Degrees;

            double G = 331.18 + 50.310482 * d;
            double H = 87.45 + 21.569231 * d;
            double corr1 = 2 * (u1 - u2);
            double corr2 = 2 * (u2 - u3);

            u1 = u1.CorrectDegreeRange();
            u2 = u2.CorrectDegreeRange();
            u3 = u3.CorrectDegreeRange();
            u4 = u4.CorrectDegreeRange();

            double r1 = 5.9057 - 0.0244 * Math.Cos(corr1.ToRadians());
            double r2 = 9.3966 - 0.0882 * Math.Cos(corr2.ToRadians());
            double r3 = 14.9883 - 0.0216 * Math.Cos(G.ToRadians());
            double r4 = 26.3627 - 0.1939 * Math.Cos(H.ToRadians());

            // Satellite I (Io)
            double x1 = r1 * Math.Sin(u1.ToRadians());
            double y1 = -r1 * Math.Cos(u1.ToRadians()) * Math.Sin(De.ToRadians());

            // Satellite II (Europa)
            double x2 = r2 * Math.Sin(u2.ToRadians());
            double y2 = -r2 * Math.Cos(u2.ToRadians()) * Math.Sin(De.ToRadians());

            // Satellite III (Ganymede)
            double x3 = r3 * Math.Sin(u3.ToRadians());
            double y3 = -r3 * Math.Cos(u3.ToRadians()) * Math.Sin(De.ToRadians());

            // Satellite IV (Callisto)
            double x4 = r4 * Math.Sin(u4.ToRadians());
            double y4 = -r4 * Math.Cos(u4.ToRadians()) * Math.Sin(De.ToRadians());

            var i = new MoonViewModel { Position = "I", Name = "Io", Date = dt, X = x1, Y = y1 };
            var ii = new MoonViewModel { Position = "II", Name = "Europa", Date = dt, X = x2, Y = y2 };
            var iii = new MoonViewModel { Position = "III", Name = "Ganymede", Date = dt, X = x3, Y = y3 };
            var iv = new MoonViewModel { Position = "IV", Name = "Callisto", Date = dt, X = x4, Y = y4 };

            return new List<MoonViewModel>
            {
                i,
                ii,
                iii,
                iv
            };
        }

        private static IEnumerable<DateTime> EachDay(DateTime startDate, DateTime endDate)
        {
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
