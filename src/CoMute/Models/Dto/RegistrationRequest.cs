﻿
using System;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models.Dto
{
    public class RegistrationRequest
    {
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
