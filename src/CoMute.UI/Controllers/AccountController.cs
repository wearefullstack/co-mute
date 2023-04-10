using CoMute.UI.Helpers;
using CoMute.UI.Models.Authentication;
using CoMute.UI.Models.Tokens;
using CoMute.UI.Models.Users;
using CoMute.UI.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoMute.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
            APIHelper client = new(configuration);
            client.InitializeClient();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> LoginRequestAsync(TokenRequestModel tokenRequest)
        {
            if (!ModelState.IsValid)
            {
                TempData["RegisterFailed"] = "Login Form Data is incorrect";
                TempData["RegisterSuccess"] = null;
                return Redirect("~/Account/Login");
            }

            var login = await userService.GetTokenAsync(tokenRequest);
            if (login.Message.Contains("FAILED."))
            {
                TempData["RegisterFailed"] = $"{login.Message}";
                TempData["RegisterSuccess"] = null;
                ViewBag.message = $"{login.Message}";
                return Redirect("~/Account/Login"); ;
            }
            else
            {
                TempData["RegisterFailed"] = null;
                TempData["RegisterSuccess"] = $"{login.Message}";
            }

            HttpContext.Session.SetString("JWToken", JsonConvert.SerializeObject(login));
            HttpContext.Session.SetString("UserName", login.UserName);
            HttpContext.Session.SetString("Email", string.IsNullOrEmpty(login.Email) ? login.UserName : login.Email);
            HttpContext.Session.SetString("UserId", login.UserId);
            HttpContext.Session.SetString("Roles", JsonConvert.SerializeObject(login.Roles));

            return Redirect("~/Home/Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RegisterRequestAsync(RegisterModel registerModel)
        {
            if (!string.IsNullOrEmpty(registerModel.Name) || !string.IsNullOrEmpty(registerModel.Surname))
                registerModel.UserName = registerModel.Name + "." + registerModel.Surname;

            if (!ModelState.IsValid)
            {
                TempData["RegisterFailed"] = $"Registration Failed due to incorrect form values";
                TempData["Registermessage"] = null;
                return Redirect("~/Account/Register");
            }

            var register = await userService.RegisterAsync(registerModel);
            if (register.ToLower().Contains("FAILED".ToLower()))
            {
                TempData["RegisterFailed"] = $"{register}";
                TempData["Registermessage"] = null;
                return Redirect("~/Account/Register");
            }
            else
            {
                TempData["Registermessage"] = $"{register}";
                TempData["RegisterFailed"] = null;
            }

            return Redirect("~/Account/Login");
        }

        public async Task<IActionResult> ProfileAsync(string Id)
        {
            var data = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(data))
                return Redirect("~/Account/Login");

            var converted = JsonConvert.DeserializeObject<AuthenticationModel>(data);

            var getUserDetails = await userService.GetUserProfileAsync(converted.UserId, converted.Token);
            if (getUserDetails == null)
                return BadRequest("Unauthenticated User");

            return View(getUserDetails);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileAsync(ProfileModel profile)
        {
            var data = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(data))
                return Redirect("~/Account/Login");

            var converted = JsonConvert.DeserializeObject<AuthenticationModel>(data);

            profile.Token = converted.Token;
            profile.UserId = converted.UserId;
            profile.UserName = Regex.Replace(profile.Name.Trim() + profile.Surname.Trim(), @"\s+", "");
            var getUserDetails = await userService.UpdateUserProfileAsync(profile);
            if(getUserDetails.ToLower().Contains("FAILED.".ToLower()))
            {
                TempData["ProfileSuccess"] = null;
                TempData["ProfileFailed"] = $"{getUserDetails}";
                return Redirect($"~/Account/Profile/{converted.UserId}");
            }
            else
            {
                TempData["ProfileFailed"] = null;
                TempData["ProfileSuccess"] = $"{getUserDetails}";
            }

            return Redirect("~/Home/Index");
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(AddRoleModel addRole)
        {
            return View();
        }
    }
}
