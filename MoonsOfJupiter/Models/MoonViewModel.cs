using System;

namespace MoonsOfJupiter.Models
{
    public class MoonViewModel
    {
        public string Position { get; set; }
        public string Name { get; set; }

        public string Symbol {  get
            {
                return Name.Substring(0, 1);
            } 
        }

        public DateTime Date { get; set; }
        public string DateDisplay { 
            get
            {
                return Date.ToShortDateString();
            } 
        }

        public double X { get; set; }
        public double Y { get; set; }

        public string CoordinateDisplay => 
            string.Format("({0}, {1})", 
            string.Format("{0:0.##}", X), 
            string.Format("{0:0.##}", Y));
    }
}
