using CoMute.Web.Controllers.Web.Helpers;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class CarPoolOpportunityController : Controller
    {
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var response = await new HttpHelper<List<CarPoolsOpportunityRequest>>()
                .GetRestServiceDataAsync("carpoolopportunity/get");
            var carPoolOpportunities = new List<CarPoolsOpportunityRequest>(response);

            var joinedCarPools = await new HttpHelper<List<JoinCarPoolsOpportunityRequest>>()
                .GetRestServiceDataAsync("carpoolopportunity/get-car-pools-opportunities");

            var identity = (ClaimsIdentity)User.Identity;
            var userId = Guid.Parse(identity.Name);

            CanJoinCarPoolOportunity(carPoolOpportunities, joinedCarPools, userId);

            return View(carPoolOpportunities);
        }

        [Authorize]
        public async Task<ActionResult> JoinedCarPoolsOpportunities()
        {
            var response = await new HttpHelper<List<CarPoolsOpportunityRequest>>()
           .GetRestServiceDataAsync("carpoolopportunity/get");
            var carPoolOpportunities = new List<CarPoolsOpportunityRequest>(response);

            var identity = (ClaimsIdentity)User.Identity;
            var userId = Guid.Parse(identity.Name);

            var joinedCarPoolsResponse = await new HttpHelper<List<JoinCarPoolsOpportunityRequest>>()
                .PostRestServiceDataAsync("carpoolopportunity/get-user-joined-car-pools-opportunities", userId);
            var joinedCarPools = new List<JoinCarPoolsOpportunityRequest>(joinedCarPoolsResponse);
        
            foreach (var joinedCarPool in joinedCarPools)
            {
                var carPoolOpportunity = carPoolOpportunities.FirstOrDefault(x=>x.CarPoolId == joinedCarPool.CarPoolId);
                joinedCarPool.Origin = carPoolOpportunity?.Origin ?? "";
                joinedCarPool.Destination = carPoolOpportunity?.Destination ?? "";                
            }                

            return View(joinedCarPools);
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Join(string id)
        {
            JoinCarPoolsOpportunityRequest joinCarPoolsOpportunityRequest = SetUpRequestDetails(id);
            await new HttpHelper<JoinCarPoolsOpportunityRequest>()
                 .PostRestServiceDataAsync("carpoolopportunity/join", joinCarPoolsOpportunityRequest);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Leave(string id)
        {
            JoinCarPoolsOpportunityRequest joinCarPoolsOpportunityRequest = SetUpRequestDetails(id);
            await new HttpHelper<JoinCarPoolsOpportunityRequest>()
                 .DeleteRestServiceDataAsync("carpoolopportunity/leave", joinCarPoolsOpportunityRequest);

            return RedirectToAction("Index");
        }

        private JoinCarPoolsOpportunityRequest SetUpRequestDetails(string id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var joinCarPoolsOpportunityRequest = new JoinCarPoolsOpportunityRequest
            {
                CarPoolId = Guid.Parse(id),
                UserId = Guid.Parse(identity.Name),    
            };
            return joinCarPoolsOpportunityRequest;
        }

        private static void CanJoinCarPoolOportunity(List<CarPoolsOpportunityRequest> carPoolOpportunities, List<JoinCarPoolsOpportunityRequest> joinedCarPools, Guid userId)
        {            
            if (carPoolOpportunities != null)
            {
                foreach (var carPool in carPoolOpportunities)
                {
                    if (joinedCarPools.Any(x => x.CarPoolId == carPool.CarPoolId && x.DateJoined != null && x.UserId == userId))
                        carPool.CanJoin = false;
                    else
                        carPool.CanJoin = true;

                    carPool.HasAvailableSeats = carPool.AvailableSeats > 0;            
                }
            }
        }
    }
}