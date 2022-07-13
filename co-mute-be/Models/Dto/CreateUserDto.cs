namespace co_mute_be.Models.Dto
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }
    }
}
