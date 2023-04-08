using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.UI.Models.Opportunity
{
    public class RegisterOpportunityModel
    {
        public string OpportunityName { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public int DaysAvailable { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public int AvailableSeats { get; set; }
        public bool IsLeader { get; set; }
        public string Notes { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime JoinedDate { get; set; }

        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
