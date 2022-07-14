using co_mute_be.Abstractions.Models;
using co_mute_be.Abstractions.Utils;
using co_mute_be.Database;
using co_mute_be.Models;
using co_mute_be.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace co_mute_be.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly DataContext _context;

        public AuthenticationController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ApiResult<User>> LoginAsync(LogingDto login)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email.Equals(login.Email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                return new ApiResult<User>()
                {
                    Error = "User not found",
                    Success = false
                };
            };

            if (!PasswordUtils.UnHashPassword(user.PasswordHash).Equals(login.Password))
            {
                return new ApiResult<User>()
                {
                    Error = "Login details invalid",
                    Success = false
                };
            }

            return new ApiResult<User>()
            {
                Success = true,
                Result = Models.User.ToFoundUser(user)
            };
        }


    }
}
