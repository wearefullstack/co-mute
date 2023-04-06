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
    public class CarPoolController : Controller
    {
        // GET: CarPool
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult CarPoolList()
        {
            IEnumerable<Car_Pool> carPools = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/carPool");
                //HTTP GET
                var responseTask = client.GetAsync("carPool");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Car_Pool>>();
                    readTask.Wait();

                    carPools = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    carPools = Enumerable.Empty<Car_Pool>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(carPools);
        }

        [HttpPost]
        public ActionResult Create(registerCarPoolRequest registerCarPoolRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/carpool");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<registerCarPoolRequest>("carpool", registerCarPoolRequest);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return View("~/Views/CarPool/Create.cshtml");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                    return View(registerCarPoolRequest);
                }
            }

        }
    }
}