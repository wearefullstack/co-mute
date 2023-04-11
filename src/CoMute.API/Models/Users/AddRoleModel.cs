using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Models.Users
{
    public class AddRoleModel
    {
        [Required]
        public string Email { get; set; }
        //[Required]
        //public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        public string userId { get; set; }
        public string roleId { get; set; }
    }
}
