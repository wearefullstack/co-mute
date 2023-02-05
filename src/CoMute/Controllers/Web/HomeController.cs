using CoMute.Web.Data;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CoMute.Web.Controllers.Web
{
    //------------------------------------------- HomeController : Amber Bruil ---------------------------------------------------------//
    public class HomeController : Controller
    {
        dbCoMuteEntities db = new dbCoMuteEntities();

        /// <summary>
        /// HTTP response to display home index view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// HTTP response to display about view
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// HttP response to display registration view
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Method to send login request data
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(LoginRequest loginRequest)
        {

            var user = db.tblRegisters.Where(zz => zz.Email == loginRequest.Email && zz.Password == loginRequest.Password).FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Credetials");
                return View();
            }
            else
            {
                TempData["ID"] = user.UserID;
                Session["ID"] = user.UserID;

                using (var client = new HttpClient())
                {
                   
                    client.BaseAddress = new Uri("http://localhost:59598/api/authentication");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync("authentication", loginRequest);
                    postTask.Wait();
                    
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Carpools");
                    }
                }
                return View(loginRequest);
            
            }
        }

        //TODO: Encrypt passwords
        /// <summary>
        /// Method to send the registration data
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegistrationRequest registrationRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/register");

                var postTask = client.PostAsJsonAsync("register", registrationRequest);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            //Error checking
            ModelState.AddModelError("Error", "Server Error. Please contact administrator.");

            return View(registrationRequest);

        }

    }
    //--------------------------------------------------- 0o00ooo End of File ooo00o0 --------------------------------------------------------//
}