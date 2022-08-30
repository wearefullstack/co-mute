using System;

namespace CoMute.Lib.Dto
{
    /// <summary>
    /// DTO for User
    /// </summary>
    public class UserDto : BaseSn
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisterTime { get; set; }
        public string FullName => Name + " " + Surname;
    }
}
