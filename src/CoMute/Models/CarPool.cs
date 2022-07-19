using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models
{
    public class CarPool
    {
        [Key]
        public int id { get; set; }
        [Required]
        [DisplayName("Departure Time")]
        public string DepartureTime { get; set; }
        [Required]
        [DisplayName("Estimated Arrival Time")]
        public string ArrivalTime { get; set; }
        [Required]
        [DisplayName("Origin")]
        public string Origin { get; set; }
        [Required]
        [DisplayName("Destination")]
        public string Destination { get; set; }

        [DisplayName("Days Available")]
        public DayOfWeek DaysAvailable { get; set; }
        [Required]
        [DisplayName("Seats Available")]
        public int SeatsAvailable { get; set; }
        [Required]
        [DisplayName("Owner/Leder")]
        public string Owner { get; set; }
        [DisplayName("Notes")]
        public string Notes { get; set; }

        public CarPool()
        {

        }
    }
}