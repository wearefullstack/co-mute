using System;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models.Dto
{
    public class UserRequest
    {
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [Display(Name="Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}