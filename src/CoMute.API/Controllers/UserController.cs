using CoMute.API.Models.Tokens;
using CoMute.API.Models.Users;
using CoMute.API.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();

                if (errors.Any())
                {
                    return BadRequest($"Ensure to enter all the required data and is in the correct format.");
                }
            }

            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();

                if (errors.Any())
                {
                    return BadRequest($"Ensure to enter all the required data and is in the correct format.");
                }
            }
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }

        [HttpPost("GetUserProfile")]
        [Authorize(Roles = "User,Lead,LeadUser")]
        public async Task<IActionResult> GetUserProfileAsync(string userId)
        {
            var result = await _userService.GetUserProfileAsync(userId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("UpdateUserProfile")]
        public async Task<ActionResult> UpdateUserProfileAsync(ProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();

                if (errors.Any())
                {
                    return BadRequest($"Ensure to enter all the required data and is in the correct format.");
                }
            }

            var result = await _userService.UpdateUserProfileAsync(model);
            return Ok(result);
        }
        /// <summary>
        /// Method is used to add or assign roles to users if needed
        /// Only an ADMIN Role will be able to access this function
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("addrole")]
        [AllowAnonymous]//[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }
    }
}
