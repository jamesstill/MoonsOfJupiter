using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoonsOfJupiter.Models
{
    public class DateRangeViewModel
    {
        public DateRangeViewModel()
        {
            MoonsForDate = new List<MoonsOnDateViewModel>();
        }

        [DataType(DataType.DateTime)]
        [Display(Name ="Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        public List<MoonsOnDateViewModel> MoonsForDate { get; set; }

        public string PositionsHeader
        {
            get
            {
                return "EAST             WEST"; // display hack
            }
        }
    }
}
