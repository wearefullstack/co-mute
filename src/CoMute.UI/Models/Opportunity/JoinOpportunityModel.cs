using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.UI.Models.Opportunity
{
    public class JoinOpportunityModel
    {
        [Required]
        public int OpportunityId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public bool IsLeader { get; set; }

        public DateTime JoinDate { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Token { get; set; }
    }
}
