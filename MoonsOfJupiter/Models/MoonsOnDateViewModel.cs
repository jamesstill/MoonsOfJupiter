using MoonsOfJupiter.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoonsOfJupiter.Models
{
    public class MoonsOnDateViewModel
    {
        public DateTime Date { get; set; }

        public string DateDisplay
        {
            get
            {
                return Date.ToShortDateString();
            }
        }

        public List<MoonViewModel> Moons { get; set; }

        public string OrientPositions
        {
            get
            {
                var spaceChar = " ";
                var length = 30;

                if (Moons == null || Moons.Count != 4)
                {
                    return string.Empty;
                }

                var sb = new StringBuilder();
                sb.Append(spaceChar.PadRight(length));
                foreach (var moon in Moons)
                {
                    var satellitePosition = moon.X.GetIntegerPart();
                    if (satellitePosition == 0)
                    {
                        // ignore transits and occultations
                        continue;
                    }
                    if (satellitePosition < 0) // precedes 
                    { 
                        sb.Remove(length + satellitePosition, 1)
                            .Insert(length + satellitePosition, moon.Symbol);
                    }
                }

                return sb.ToString();
            }
        }

        public string OccidentPositions
        {
            get
            {
                var spaceChar = " ";
                var length = 30;

                if (Moons == null || Moons.Count != 4)
                {
                    return string.Empty;
                }

                var sb = new StringBuilder();
                sb.Append(spaceChar.PadRight(length));
                foreach (var moon in Moons)
                {
                    var satellitePosition = moon.X.GetIntegerPart();
                    if (satellitePosition == 0)
                    {
                        // ignore transits and occultations
                        continue;
                    }
                    if (satellitePosition > 0) // follows 
                    {
                        sb.Remove(satellitePosition, 1)
                            .Insert(satellitePosition, moon.Symbol);
                    }
                }

                return sb.ToString();
            }
        }
    }
}
