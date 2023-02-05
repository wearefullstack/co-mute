
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models.Dto
{
    //------------------------------------------- RegistrationRequest : Amber Bruil ---------------------------------------------------------//
    public class RegistrationRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password do not match.")] public string ConfirmPassword { get; set; }

    }
    //--------------------------------------------------- 0o00ooo End of File ooo00o0 --------------------------------------------------------//

}
