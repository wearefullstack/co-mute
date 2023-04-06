﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Models.Users
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
        [Required]
        public string Password { get; set; }
    }
}
