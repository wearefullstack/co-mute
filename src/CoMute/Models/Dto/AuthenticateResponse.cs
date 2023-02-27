using System;

namespace CoMute.Web.Models.Dto
{
    public class AuthenticateResponse
    {
        public Guid UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string JwtToken { get; set; }
        public User userObject { get; set; }
        public Boolean IsResult { get; set; }

    }
}