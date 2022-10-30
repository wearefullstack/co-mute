using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public async Task<ActionResult> Index()
        {
            if (TempData.ContainsKey("LoginRequest"))
            {
                LoginRequest loginRequest = (LoginRequest)TempData["LoginRequest"];
                HttpResponseMessage response = await UserService.LoginUser(loginRequest);
                if (response.IsSuccessStatusCode)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    ModelState.AddModelError("", "Login fail");
                    return View(loginRequest);
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Index([FromBody]LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody]RegistrationRequest registrationRequest)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await UserService.RegisterUser(registrationRequest);
                if (response.IsSuccessStatusCode)
                {
                    string user = await response.Content.ReadAsStringAsync();
                    JObject userObject = JObject.Parse(user);
                    LoginRequest loginRequest = new LoginRequest()
                    {
                        Email = userObject.Value<string>("EmailAddress"),
                        Password = userObject.Value<string>("Password")
                    };
                    TempData["LoginRequest"] = loginRequest;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed");
                    return View(registrationRequest);
                }
            }
            return View(registrationRequest);
        }
    }
}