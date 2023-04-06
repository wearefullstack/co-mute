using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Models.Opportunity
{
    public class RegisterOpportunityModel
    {
        [Required]
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

        [Required]
        public bool IsLeader { get; set; }
        public string Notes { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
