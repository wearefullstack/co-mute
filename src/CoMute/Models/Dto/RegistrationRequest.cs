
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models.Dto
{
    public class RegistrationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }


}
