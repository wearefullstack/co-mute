using comute.DTOs;
using comute.Models;
using comute.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace comute.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : Controller
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUserService _userService;
    public LoginController(IOptions<JwtSettings> jwtSettings, IUserService userService)
    {
        _jwtSettings = jwtSettings.Value;
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(Login login)
    {
        var user = await Authenticate(login);
        if (user != null)
        {
            var token = Generate(user);
            return Ok(new { token = token, user = user });
        }
        return NotFound("User not found");
    }

    [NonAction]
    private string Generate(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]{
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            }),
            Expires = DateTime.Now.AddMinutes(1440),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    [NonAction]
    private async Task<User> Authenticate(Login login)
    {
        List<User> users = await _userService.Users();
        var currentUser = users.FirstOrDefault(user => user.Email.ToLower()
        == login.Email.ToLower() && user.Password == login.Password);

        if (currentUser != null)
        {
            return currentUser;
        }
        return null;
    }
}
