using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Device.Location;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models
{
    public class Carpool
    {
        public int CarpoolID { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime ETA { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public int iAvailableSeats { get; set; } = 0;
        [Required]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PassengerIds { get; set; }
        public string Notes { get; set; }

        public Carpool ShallowCopy()
        {
            return (Carpool)this.MemberwiseClone();
        }
    }

    
}