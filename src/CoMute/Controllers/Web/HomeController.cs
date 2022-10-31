using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using AllowAnonymousAttribute = System.Web.Mvc.AllowAnonymousAttribute;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController() { }

        public async Task<ActionResult> Index()
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            HttpResponseMessage response = await ComuteService.GetCarPoolMemberships(user.Id);
            if (response.IsSuccessStatusCode)
            {
                List<CarPoolDto> joinedCarPools = new List<CarPoolDto>();
                ICollection<CarPoolMembership> memberships = await response.Content.ReadAsAsync<ICollection<CarPoolMembership>>();
                if(memberships != null)
                {
                    foreach (CarPoolMembership membership in memberships)
                    {
                        HttpResponseMessage responseMessage = await ComuteService.GetCarPool(membership.CarPoolId);
                        if (response.IsSuccessStatusCode)
                        {
                            CarPoolDto carPoolDto = await responseMessage.Content.ReadAsAsync<CarPoolDto>();
                            carPoolDto.DateJoined = membership.DateJoined;
                            carPoolDto.CarPoolMembershipId = membership.Id;
                            joinedCarPools.Add(carPoolDto);
                        }
                    }
                    return View(joinedCarPools);
                }
            }
            return View();
        }

        public async Task<ActionResult> LeaveCarPool(int carpoolMembershipId)
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

    }

}