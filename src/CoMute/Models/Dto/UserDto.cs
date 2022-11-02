using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    [Serializable]
    public class UserDto
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password should have 8 characters minimum", MinimumLength = 8)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password should have 8 characters minimum", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public ICollection<CarPool> CarPools { get; set; }
        public ICollection<CarPoolMembership> CarPoolMemberships { get; set; }
    }
}