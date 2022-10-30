using CoMuteCore.Models;
using CoMuteCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoMuteCore.Controllers.Client
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register() 
        {
            return View();
        }

        public IActionResult Profile() 
        {
            return View();
        }

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
