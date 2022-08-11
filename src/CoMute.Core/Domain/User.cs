using System;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Core.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
