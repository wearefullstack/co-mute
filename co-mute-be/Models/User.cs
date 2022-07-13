using co_mute_be.Abstractions.Utils;
using co_mute_be.Models.Dto;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace co_mute_be.Models
{
    [Table("Users")]
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public string PasswordHash { get; set; }

        public static User FromDto(CreateUserDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = PasswordUtils.HashPassword(dto.Password)
            };
            return user;
        }

        public static User ToFoundUser(User user)
        {
            user.PasswordHash = null;
            return user;
        }

    }
}
