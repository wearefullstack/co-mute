using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoMute.Interfaces;
using CoMute.Models;
using CoMute.Models.Requests;

namespace CoMute.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    [Route("{userId}")]
    public IActionResult GetUser(int userId)
    {
      try
      {
        var user = _userService.GetUser(userId);

        if (user != null)
        {
          return Ok(user);
        }
        else
        {
          return BadRequest("User not found");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest model)
    {

      try
      {
        var response = await _userService.UpdateUserAsync(model);

        if (response > 0)
        {
          return Ok(new ContentResult { StatusCode = (int)HttpStatusCode.OK, ContentType = "application/json", Content = "User  was updated successfully" });
        }
        else
        {
          return BadRequest("Something went wrong, error updating user");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] User model)
    {

      try
      {
        var user = _userService.RegisterUser(model);

        if (user != null)
        {
          return Ok(new ContentResult
          {
            StatusCode = (int)HttpStatusCode.OK,
            ContentType = "application/json",
            Content = $"{user.Name} {user.Surname} was added successfully"
          });
        }
        else
        {
          return BadRequest("Something went wrong, user registration was unsuccessful");
        }
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
