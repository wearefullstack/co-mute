
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models.Dto
{
    public sealed class LoginRequest
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
