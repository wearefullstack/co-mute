using System.ComponentModel.DataAnnotations;

namespace Co_Mute.Api.Models.Dto
{
    public class UpdateUser
    {
        [Required(ErrorMessage = "UserId required")]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Phone]
        public string Phone { get; set; } = string.Empty;
        [Required(ErrorMessage = "password required")]
        public string Password { get; set; } = string.Empty;
    }
}
