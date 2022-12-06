using System.ComponentModel.DataAnnotations;

namespace Co_Mute.Models.Profile
{
    public class ChangePasswordPostModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
