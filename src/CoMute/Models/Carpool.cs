using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class Carpool
    {
        public int CarpoolID { get; set; }
        [Required]
        public DateTime DepartTime { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string DaysAvail { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public int AvailSeats { get; set; } = 0;
        [Required]
        public string UserID { get; set; }
        [Required]
        public string OwnerLeader { get; set; }
        [Required]
        public string PassengerIDs { get; set; }
        public string Notes { get; set; }
    }
}