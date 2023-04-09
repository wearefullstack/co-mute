using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.UI.Models.Users
{
    public class ProfileModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string CustomPhone { get; set; }
        [Required]
        public string CustomEmail { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
