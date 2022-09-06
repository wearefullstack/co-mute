using System.ComponentModel.DataAnnotations;

namespace Co_Mute.Api.Models.Dto
{
    public class UserLogin
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; } = string.Empty;
    }
}
