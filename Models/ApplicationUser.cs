using Microsoft.AspNetCore.Identity;
using Co_Mute.Data;

namespace Co_Mute.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; } = "Active";
    }
}
