using System;

namespace CoMute.Core.Domain
{
    public class JoinCarPoolsOpportunity
    {
        public Guid JoinCarPoolsOpportunityId { get; set; }
        public Guid CarPoolId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
