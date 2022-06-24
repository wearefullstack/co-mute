
namespace CoMute.Web.Models.Dto
{
    public class RegistrationRequest // Register new user
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; } // Bad practise to save passwords in a database, just for assessment purposes
    }
}
