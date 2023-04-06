using CoMute.Web.Models;
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

        public ActionResult CarPoolList()
        {
            IEnumerable<Car_Pool> carPools = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/home");
                //HTTP GET
                var responseTask = client.GetAsync("home");
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
    }
}