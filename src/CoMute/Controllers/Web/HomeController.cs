using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using AllowAnonymousAttribute = System.Web.Mvc.AllowAnonymousAttribute;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController() { }

        public async Task<ActionResult> Index()
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            List<CarPoolDto> memberships = new List<CarPoolDto>();
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            HttpResponseMessage response = await ComuteService.GetCarPoolMemberships(user.Id);
            if (response.IsSuccessStatusCode)
            {
                List<CarPoolDto> data = await response.Content.ReadAsAsync<List<CarPoolDto>>();
                if(data != null)
                {
                    memberships.AddRange(data);
                }
            }
            return View(memberships);
        }

        public async Task<ActionResult> Leave(int carpoolMembershipId)
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            HttpResponseMessage responseMessage = await ComuteService.LeaveCarPool(user.Id, carpoolMembershipId);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Staus"] = "Left car pool";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Staus"] = "Something went wrong";
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Join(int carpoolId, DateTime arrival, DateTime departure)
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            List<CarPoolDto> memberships;
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            HttpResponseMessage response = await ComuteService.GetCarPoolMemberships(user.Id);
            if (response.IsSuccessStatusCode)
            {
                memberships = await response.Content.ReadAsAsync<List<CarPoolDto>>();
                if (memberships != null)
                {
                    foreach(CarPoolDto carPoolDto in memberships)
                    {
                        bool overlap = departure < carPoolDto.ExpectedArrivalTime && carPoolDto.DepartureTime < arrival;
                        if (overlap)
                        {
                            TempData["Status"] = "Could not join. Car pool time range overlaps with existing car pool that you have already joined.";
                            return RedirectToAction("Index", "Search");
                        }
                    }
                }
                CarPoolMembership carPoolMembership = new CarPoolMembership()
                {
                    UserId = user.Id,
                    CarPoolId = carpoolId
                };
                HttpResponseMessage message = await ComuteService.JoinCarPool(carPoolMembership);
                if (message.IsSuccessStatusCode)
                {
                    TempData["Status"] = "Car pool joined";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Status"] = "Could not join car pool";
                    return RedirectToAction("Index", "Search");
                }
            }
            return RedirectToAction("Index", "Search");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CarPoolDto());
        }
        
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CarPoolDto carPoolDto)
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            
            carPoolDto.UserId = user.Id;
            carPoolDto.AvailableSeats = carPoolDto.MaximumSeats;

            if (ModelState.IsValid)
            {
                HttpResponseMessage responseMessage =  await ComuteService.GetUserCarPools(user.Id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    //check that times do not overlap
                    List<CarPoolDto> carPools = await responseMessage.Content.ReadAsAsync<List<CarPoolDto>>();
                    if (carPools != null)
                    {
                        foreach(CarPoolDto carPool in carPools)
                        {
                            bool overlap = carPoolDto.DepartureTime < carPool.ExpectedArrivalTime && carPool.DepartureTime < carPoolDto.ExpectedArrivalTime;

                            if (overlap)
                            {
                                ModelState.AddModelError("", "Given time range overlaps with existing time range");
                                return View(carPoolDto);
                            }
                        }
                    }

                    //add available days
                    if(carPoolDto.SelectedDays.Count() > 0)
                    {
                        List<AvailableDayDto> availableDays = new List<AvailableDayDto>();
                        foreach(int day in carPoolDto.SelectedDays)
                        {
                            availableDays.Add(new AvailableDayDto()
                            {
                                Day = (DayEnumeration)day
                            });
                        }
                        carPoolDto.AvailableDays = availableDays;
                        HttpResponseMessage message = await ComuteService.CreateCarPool(carPoolDto);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return RedirectToAction("CarPools");
                        }
                        else
                        {
                            ModelState.AddModelError("", message.ReasonPhrase);
                            return View(carPoolDto);
                        }
                    }
                    else
                    {
                       ModelState.AddModelError("", "At least one available day must be selected");
                        return View(carPoolDto);
                    }

                }
            }

            return View(carPoolDto);
        }

        public async Task<ActionResult> CarPools()
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            List<CarPoolDto> carPools = new List<CarPoolDto>();
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            HttpResponseMessage response = await ComuteService.GetUserCarPools(user.Id);
            if (response.IsSuccessStatusCode)
            {
                List<CarPoolDto> carPoolDtos = await response.Content.ReadAsAsync<List<CarPoolDto>>();
                if(carPoolDtos != null)
                {
                    carPools.AddRange(carPoolDtos);
                }
            }
            return View(carPools);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
    }
}