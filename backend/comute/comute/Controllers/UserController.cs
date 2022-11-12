using comute.client.User;
using comute.Models;
using comute.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace comute.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) =>
        _userService = userService;


    [HttpGet("all")]
    public async Task<IActionResult> Users()
    {
        List<User> users = await _userService.Users();
        return Ok(users);
    }

    [HttpGet("currentUser/{userId:int}")]
    public async Task<IActionResult> CurrentLoggedInUser(int userId)
    {
        var user = await _userService.CurrentUser(userId);
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("save/{userId:int}")]
    public async Task<IActionResult> SaveUserDetails(UserRequest request, int userId = 0)
    {
        try
        {
            var user = AddUser(request, userId);
            var allUsers = await _userService.Users();
            bool isDuplicate = false;

            if (userId == 0)
            {
                foreach (var item in allUsers)
                {
                    if (user.Email == item.Email)
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                if (!isDuplicate)
                    await _userService.RegisterUser(userId, user);
            }
            else
                await _userService.RegisterUser(userId, user);

            var response = !isDuplicate ? UserResponseBack(user) : new User();

            return CreatedAtAction(
                actionName: nameof(CurrentLoggedInUser),
                routeValues: new { userId = user.UserId },
                value: response
            );
        }
        catch
        {
            return Problem();
        }
    }

    [NonAction]
    private static User AddUser(UserRequest request, int userId)
    {
        return new User
        {
            UserId = userId,
            Name = request.Name,
            Surname = request.Surname,
            Phone = request.Phone,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role,
            CreatedOn = request.CreatedOn
        };
    }

    [NonAction]
    private static User UserResponseBack(User user)
    {
        try
        {
            return new User
            {
                UserId = user.UserId,
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.Phone,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                CreatedOn = user.CreatedOn
            };
        }
        catch
        {
            return new User();
        }
    }

}
