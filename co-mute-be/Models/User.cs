namespace co_mute_be.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        // Don't do this in production!
        public string Password { get; set; }

        //Do this rather
        public string PasswordHash { get; set; }

    }
}
