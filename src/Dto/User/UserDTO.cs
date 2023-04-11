namespace FSWebApi.Dto.User
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Phone { get; set; }
    }
}
