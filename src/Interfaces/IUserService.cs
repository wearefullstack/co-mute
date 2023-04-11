using FSWebApi.Dto.Auth;
using FSWebApi.Dto.User;
using FSWebApi.Models;

namespace FSWebApi.Interfaces
{
    public interface IUserService
    {
        public AuthResponse Authenticate(AuthRequest authRequest);
        public IEnumerable<UserDTO> GetUsers();
        public User GetById(Guid userId);
        public UserDTO AddUser(CreateUserDTO user);
    }
}
