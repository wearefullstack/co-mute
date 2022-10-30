using CoMuteCore.Models;
using CoMuteCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoMuteCore.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                if (user != null)
                {
                    await _userService.CreateUserAsync(user);
                    return Ok();
                }
            }
            catch (NullReferenceException ex)
            {

            }

            return BadRequest();
        }


        [HttpGet]
        public async Task<IActionResult> GetUserAsync(int Id)
        {
            try
            {
                var user = await _userService.GetUserAsync(Id);
                return Ok(user);
            }
            catch (NullReferenceException ex)
            {

            }

            return NotFound();
        }
    }
}
