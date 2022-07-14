using co_mute_be.Abstractions.Utils;
using co_mute_be.Models.Dto;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace co_mute_be.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public string PasswordHash { get; set; }

        //Relationships
        public List<CarPoolOpp> CarPoolOpps { get; set; } = new List<CarPoolOpp>();

        #region Utils
        public static User FromDto(CreateUserDto dto)
        {
            var user = new User
            {
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
        #endregion
    }
}
