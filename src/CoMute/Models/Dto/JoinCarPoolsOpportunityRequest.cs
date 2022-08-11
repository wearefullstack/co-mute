using System;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models.Dto
{
    public class JoinCarPoolsOpportunityRequest
    {
        public Guid JoinCarPoolsOpportunityId { get; set; }
        public Guid CarPoolId { get; set; }
        public Guid UserId { get; set; }
        [Display(Name="Date Joined")]
        public DateTime? DateJoined { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}