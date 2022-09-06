namespace Co_Mute.Api.Models
{
    public class UserDetail : User
    {
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string? PassengerNote { get; set; } = string.Empty;
    }
}
