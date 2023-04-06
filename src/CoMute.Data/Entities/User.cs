using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Data.Entities
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string CustomPhone { get; set; }
        public string CustomEmail { get; set; }
        public string Password { get; set; }

        //public IEnumerable<CarPoolOpportunity> CarPoolOpportunities { get; set; }
        public IEnumerable<UserOpportunity> UserOpportunities { get; set; }
    }
}
