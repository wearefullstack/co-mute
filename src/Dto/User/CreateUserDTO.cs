using System.ComponentModel.DataAnnotations;

namespace FSWebApi.Dto.User
{
    public class CreateUserDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
    }
}
