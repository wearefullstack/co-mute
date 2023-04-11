using FSWebApi.Authorization;
using FSWebApi.Dto.Auth;
using FSWebApi.Dto.User;
using FSWebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FSWebApi.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("authenticate")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<AuthResponse> Authenticate(AuthRequest authRequest)
        {
            var response = _userService.Authenticate(authRequest);
            return Ok(response);
        }

        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<UserDTO> RegisterUser(CreateUserDTO user)
        {
            //TODO: Automatically log user in and generte JWB on registration

            var createdUser = _userService.AddUser(user);
            return Created(createdUser.UserId.ToString(), createdUser);


        }


        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

    }
}
