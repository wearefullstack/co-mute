using CoMute.Web.Controllers.Web.Helpers;
using CoMute.Web.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class CarPoolOpportunityController : Controller
    {
        [Authorize]
        // GET: CarPoolOpportunity
        public async Task<ActionResult> Index()
        {
            var response = await new HttpHelper<List<CarPoolsOpportunityRequest>>()
                .GetRestServiceDataAsync("carpoolopportunity/get");
            var carPoolOpportunities = new List<CarPoolsOpportunityRequest>(response);
            return View(carPoolOpportunities);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}