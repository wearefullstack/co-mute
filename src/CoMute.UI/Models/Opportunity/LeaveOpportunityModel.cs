using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.UI.Models.Opportunity
{
    public class LeaveOpportunityModel
    {
        [Required]
        public int OpportunityId { get; set; }

        [Required]
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
