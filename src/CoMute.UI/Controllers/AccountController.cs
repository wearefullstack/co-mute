using CoMute.UI.Helpers;
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
using System.Threading.Tasks;

namespace CoMute.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public AccountController(IUserService userService,IConfiguration configuration)
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
        public async Task<IActionResult> LoginRequestAsync(TokenRequestModel tokenRequest)
        {          
            if (!ModelState.IsValid)
                return Redirect("~/Account/Login");

            var login = await userService.GetTokenAsync(tokenRequest);
            if (login.Message.Contains("FAILED."))
            {
                ViewBag.message = $"{login.Message}";
                return Redirect("~/Account/Login");
            }

            HttpContext.Session.SetString("JWToken", JsonConvert.SerializeObject(login));
            HttpContext.Session.SetString("UserName", login.UserName);

            return Redirect("~/Home/Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterRequestAsync(RegisterModel registerModel)
        {
            if(!string.IsNullOrEmpty(registerModel.Name) || !string.IsNullOrEmpty(registerModel.Surname))
            registerModel.UserName = registerModel.Name + "." + registerModel.Surname;

            if (!ModelState.IsValid)
                return Redirect("~/Account/Register");

            var register = await userService.RegisterAsync(registerModel);
            if (register.Contains("FAILED."))
            {
                TempData["Registermessage"] = $"{register}";
                return Redirect("~/Account/Register");
            }
            else
                TempData["Registermessage"] = $"{register}";

            return Redirect("~/Account/Login");
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
