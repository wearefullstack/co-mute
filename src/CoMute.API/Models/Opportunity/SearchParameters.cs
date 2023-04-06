using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Models.Opportunity
{
    public class SearchParameters
    {
        public string SearchType { get; set; }
        public string OpportunityName { get; set; }
        public string Destination { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime JoinDate { get; set; }
        public string Origin { get; set; }
    }
}
