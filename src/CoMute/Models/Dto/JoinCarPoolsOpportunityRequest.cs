using System;

namespace CoMute.Web.Models.Dto
{
    public class JoinCarPoolsOpportunityRequest
    {
        public Guid JoinCarPoolsOpportunityId { get; set; }
        public Guid CarPoolId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateJoined { get; set; }
    }
}