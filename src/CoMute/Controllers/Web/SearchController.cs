using CoMute.Web.Controllers.Web.Helpers;
using CoMute.Web.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class SearchController : Controller
    {
        // GET: Search
        public async Task<ActionResult> Index()
        {
            var response = await new HttpHelper<List<CarPoolsOpportunityRequest>>()
                .GetRestServiceDataAsync("carpoolopportunity/get");
            var carPoolOpportunities = new List<CarPoolsOpportunityRequest>(response);

            return View(carPoolOpportunities);
        }

        [HttpPost]
        public async Task<ActionResult> Index(string searchTerm)
        {
            var response = await new HttpHelper<List<CarPoolsOpportunityRequest>>()
           .GetRestServiceDataAsync("carpoolopportunity/get");
            var carPoolOpportunities = new List<CarPoolsOpportunityRequest>(response);

            searchTerm = searchTerm.ToLower();
            if (!string.IsNullOrEmpty(searchTerm))
                carPoolOpportunities = carPoolOpportunities
                    .Where(x => x.Origin.ToLower() == searchTerm
                    || x.Destination.ToLower() == searchTerm).ToList();

            return View(carPoolOpportunities);
        }
    }
}