using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Models.Opportunity
{
    public class SearchOpportunityModel
    {
        public int OpportunityId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OpportunityName { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Origin { get; set; }
        public int DaysAvailable { get; set; }
        public string Destination { get; set; }
        public int AvailableSeats { get; set; }
        public string Notes { get; set; }
        public string IsLeader { get; set; }
        public string CreatedDate { get; set; }
        public string JoinDate { get; set; }
    }
}
