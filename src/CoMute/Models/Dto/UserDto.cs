using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    [Serializable]
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<CarPool> CarPools { get; set; }
        public ICollection<CarPoolMembership> CarPoolMemberships { get; set; }
    }
}