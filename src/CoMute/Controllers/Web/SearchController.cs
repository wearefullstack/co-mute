using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            
            return View();
        }

        public async Task<ActionResult> CarPools()
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            List<CarPoolDto> carPools = new List<CarPoolDto>();
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            HttpResponseMessage response = await ComuteService.Filter(user.Id);
            if (response.IsSuccessStatusCode)
            {
                List<CarPoolDto> carPoolDtos = await response.Content.ReadAsAsync<List<CarPoolDto>>();
                if (carPoolDtos != null)
                {
                    carPools.AddRange(carPoolDtos);
                }
            }
            return View("_CarPools", carPools);
        }

        public async Task<ActionResult> Filter(string origin, string destination)
        {

            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            List<CarPoolDto> carPools = new List<CarPoolDto>();
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            HttpResponseMessage response = await ComuteService.Filter(user.Id, origin, destination);
            if (response.IsSuccessStatusCode)
            {
                List<CarPoolDto> carPoolDtos = await response.Content.ReadAsAsync<List<CarPoolDto>>();
                if (carPoolDtos != null)
                {
                    carPools.AddRange(carPoolDtos);
                }
            }
            return View("_CarPools", carPools);
        }
    }
}