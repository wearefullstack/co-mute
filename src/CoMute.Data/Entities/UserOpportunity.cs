using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Data.Entities
{
    public class UserOpportunity
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int OpportunityId { get; set; }
        public User User { get; set; }
        public CarPoolOpportunity CarPoolOpportunity { get; set; }
        public bool IsLeader { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
