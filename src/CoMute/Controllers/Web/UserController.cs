using CoMute.Web.Models.Dto;
using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class UserController : Controller

    {
        public static User currentUser = new User();
        // GET: User
        public ActionResult Profile()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/user");
                //HTTP GET
                var responseTask = client.GetAsync("user?EmailAdress=" + HomeController.email);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                }
                else //web api sent error response 
                {
                    //log response status here..

                    currentUser = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(currentUser);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationRequest registrationRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/user");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<RegistrationRequest>("user", registrationRequest);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return View("~/Views/Home/Index.cshtml");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                    return View(registrationRequest);
                }
            }

        }

    }
}
