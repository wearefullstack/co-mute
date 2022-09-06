using Co_Mute.Api.Models;
using Co_Mute.Api.Models.Dto;
using Co_Mute.Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Co_Mute.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository iUserRepository) => _userRepository = iUserRepository;

        [HttpPost]
        public async Task<ActionResult> RegisterNewUser([FromBody] UserRegisterDto oCreateUser)
        {
            try
            {
                await _userRepository.RegisterNewUser(oCreateUser);
                return Ok(new { message = "added User" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public async Task<ActionResult<FunctionCommandUser>> LoginUser([FromBody] UserLogin oUserLogin)
        {
            try
            {
                var user = await _userRepository.LoginUser(oUserLogin);

                if (user is null)
                {
                    return BadRequest(new { message = "Invalid credentials" });
                }

                string sToken = _userRepository.CreateToken(oUserLogin);
                user.Token = sToken;
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateUserDetails([FromBody] UpdateUser updateUser, int id)
        {
            try
            {
                var user = await _userRepository.UpdateUserDetails(updateUser, updateUser.UserId = id);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

      
    }
}
