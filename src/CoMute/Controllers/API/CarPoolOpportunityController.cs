using AutoMapper;
using CoMute.Core.Domain;
using CoMute.Core.Interfaces.Repositories;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolOpportunityController : ApiController
    {
        private readonly IMappingEngine _mappingEngine;
        private readonly IUserRepository _userRepository;
        private readonly ICarPoolOpportunityRepository _carPoolOpportunityRepository;

        public CarPoolOpportunityController(ICarPoolOpportunityRepository carPoolOpportunityRepository, IMappingEngine mappingEngine, IUserRepository userRepository)
        {
            _carPoolOpportunityRepository = carPoolOpportunityRepository ?? throw new ArgumentNullException(nameof(carPoolOpportunityRepository));
            _mappingEngine = mappingEngine ?? throw new ArgumentNullException(nameof(mappingEngine));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        [Route("api/carpoolopportunity/get")]
        public HttpResponseMessage Get()
        {
            var carPools = GetCarPools();
            return Request.CreateResponse(HttpStatusCode.OK, carPools);
        }

        [HttpPost]
        [Route("api/carpoolopportunity/add")]
        public HttpResponseMessage Post(CarPoolsOpportunityRequest carPoolsOpportunityRequest)
        {
            var identity = (ClaimsIdentity)User.Identity;

            if (ModelState.IsValid)
            {
                var carPools = GetCarPools();

                carPoolsOpportunityRequest.DateCreated = DateTime.UtcNow;
                carPoolsOpportunityRequest.CarPoolId = Guid.NewGuid();
                carPoolsOpportunityRequest.OwnerOrLeader = identity.Name;
                var carPool = _mappingEngine.Map<CarPoolOpportunity>(carPoolsOpportunityRequest);
                bool? overlap = PeriodsOverLap(carPool, carPools);

                if (overlap == false)
                {
                    _carPoolOpportunityRepository.Save(carPool);
                    return Request.CreateResponse(HttpStatusCode.Created, carPool);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("api/carpoolopportunity/join")]
        public HttpResponseMessage Post(JoinCarPoolsOpportunityRequest joinCarPoolsOpportunityRequest)
        {
            /**
             * when joining subtract the available car seats 
             * when leaving add 1 to the car seats available
             **/

            var carPool = new CarPoolOpportunity();
            if (ModelState.IsValid)
            {
                var carPools = GetCarPools(); //check ids
                carPool.DateCreated = DateTime.UtcNow;

                //carPoolsOpportunityRequest.DateCreated = DateTime.UtcNow;
                //carPoolsOpportunityRequest.CarPoolId = Guid.NewGuid();
                //carPoolsOpportunityRequest.OwnerOrLeader = userId;
                //carPool = _mappingEngine.Map<CarPoolOpportunity>(carPoolsOpportunityRequest);
                //bool? overlap = PeriodsOverLap(carPool, carPools);

                //if (overlap == false)
                //{
                //    _carPoolOpportunityRepository.Save(carPool);
                //    return Request.CreateResponse(HttpStatusCode.Created, carPool);
                //}
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        private List<CarPoolOpportunity> GetCarPools()
        {
            var users = _userRepository.GetAllUsers();
            var carPools = _carPoolOpportunityRepository.GetAllCarPools();
            foreach (var carPool in carPools)
            {
                carPool.OwnerOrLeader = users.FirstOrDefault(x => x.UserId == Guid.Parse(carPool.OwnerOrLeader)).Name;
            }
            return carPools;
        }

        private static bool? PeriodsOverLap(CarPoolOpportunity carPool, List<CarPoolOpportunity> carPools)
        {
            return carPools?.Any(x => x.DepartureTime < carPool.ExpectedArrivalTime && x.ExpectedArrivalTime < carPool.DepartureTime);
        }
    }
}