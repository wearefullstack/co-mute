using FSWebApi.Data;
using FSWebApi.Dto.User;
using FSWebApi.Interfaces;
using FSWebApi.Models;
using FSWebApi.Utils.ErrorHandling;
using BCrypt.Net;
using FSWebApi.Dto.Auth;
using FSWebApi.Authorization;

namespace FSWebApi.Services
{
    public class UserService : IUserService
    {
        AppDbContext _context;
        private IJwtUtils _jwtUtils;

        public UserService(AppDbContext context, IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        public AuthResponse Authenticate(AuthRequest authRequest)
        {
            var user = _context.users.Where(x => x.Email == authRequest.Email).FirstOrDefault();

            if (user == null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user.Password))
                throw new AppException("Username or password is incorrect");

            AuthResponse res = new AuthResponse()
            {
                Email = user.Email,
                Phone = user.Phone,
                Name = user.Name,
                Surname = user.Surname,
                UserId = user.UserId,
            };

            res.Token = _jwtUtils.GenerateToken(user);
            return res;
        }

        public User GetById(Guid userId)
        {
            var user = _context.users.Where(x => x.UserId == userId).FirstOrDefault();

            if (user == null)
                throw new AppException("User not found");


            return user;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var users = _context.users.ToList();

            List<UserDTO> res = new List<UserDTO>();

            users.ForEach(x =>
            {
                res.Add(MapUserToUserDTO(x));
            });

            return res;
        }

        public UserDTO AddUser(CreateUserDTO user)
        {
            if(_context.users.Any(x => x.Email == user.Email))
                throw new AppException("User with the email '" + user.Email + "' already exists");

            User newUser = new User()
            {
                UserId = Guid.NewGuid(),
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                Surname = user.Surname,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            };

            _context.users.Add(newUser); 
            _context.SaveChanges();

            UserDTO res = MapUserToUserDTO(newUser);
            return res;
        }

        private UserDTO MapUserToUserDTO(User user)
        {
            UserDTO res = new UserDTO()
            {
                Email = user.Email,
                Phone = user.Phone,
                Name = user.Name,
                Surname = user.Surname,
                UserId = user.UserId,
            };

            return res;
        }



    }
}
