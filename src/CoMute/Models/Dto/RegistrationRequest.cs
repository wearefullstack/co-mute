
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models.Dto
{
    public class RegistrationRequest
    {
        public int RegisterID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password doesn't match.")] public string ConfirmPassword { get; set; }
    }
}
