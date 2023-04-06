using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        public static string email = "";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

       

        public ActionResult CarPoolList()
        {
            //IEnumerable<StudentViewModel> students = null;
            List<Car_Pool> car_Pools = new List<Car_Pool>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/home");
                //HTTP GET
                var responseTask = client.GetAsync("home");
                responseTask.Wait();

            

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Car_Pool>>();
                    readTask.Wait();

                    car_Pools = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    car_Pools = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(car_Pools);
        } 

        [HttpPost]
        public ActionResult Index(LoginRequest loginRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/home");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<LoginRequest>("home", loginRequest);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    email = loginRequest.Email;
                    return View("~/Views/User/Profile.cshtml");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                    return View(loginRequest);
                }
            }

        }



    }
}