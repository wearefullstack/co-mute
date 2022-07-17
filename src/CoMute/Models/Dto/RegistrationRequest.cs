using System.Data.Entity;

namespace CoMute.Web.Models.Dto
{
    public class CoMute : DbContext
    {
        DbSet<RegistrationRequest> RegistrationRequest { get; set; }
    }
    public class RegistrationRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
