using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models
{
    public class User
    { 
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public virtual ICollection<CarPool> CarPools { get; set; }
        public virtual ICollection<CarPoolMembership> CarPoolMemberships { get; set; }
    }
}
