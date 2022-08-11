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
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolOpportunityController : ApiController
    {
        private readonly IMappingEngine _mappingEngine;
        private readonly IUserRepository _userRepository;
        private readonly ICarPoolOpportunityRepository _carPoolOpportunityRepository;
        private readonly IJoinedCarPoolsOpportunityRepository _joinedCarPoolsOpportunityRepository;

        public CarPoolOpportunityController(ICarPoolOpportunityRepository carPoolOpportunityRepository, IMappingEngine mappingEngine, IUserRepository userRepository, IJoinedCarPoolsOpportunityRepository joinedCarPoolsOpportunityRepository)
        {
            _carPoolOpportunityRepository = carPoolOpportunityRepository ?? throw new ArgumentNullException(nameof(carPoolOpportunityRepository));
            _mappingEngine = mappingEngine ?? throw new ArgumentNullException(nameof(mappingEngine));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _joinedCarPoolsOpportunityRepository = joinedCarPoolsOpportunityRepository ?? throw new ArgumentNullException(nameof(joinedCarPoolsOpportunityRepository));
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
            if (ModelState.IsValid)
            {
                List<CarPoolsOpportunityRequest> carPools = SetUp(joinCarPoolsOpportunityRequest, isCreate: true);

                var joinCarPoolsOpportunity = Map(joinCarPoolsOpportunityRequest);

                CarPoolOpportunity carPool = GetCarPoolOpportunityById(joinCarPoolsOpportunityRequest);
                bool? overlap = PeriodsOverLap(carPool, carPools);

                if (overlap == false)
                {
                    carPool.AvailableSeats -= 1;
                    Update(carPool);
                    _joinedCarPoolsOpportunityRepository.Save(joinCarPoolsOpportunity);
                    return Request.CreateResponse(HttpStatusCode.Created, joinCarPoolsOpportunity);
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("api/carpoolopportunity/get-car-pools-opportunities")]
        public HttpResponseMessage GetJoinedCarPoolsOpportunities()
        {
            var carPools = _joinedCarPoolsOpportunityRepository.GetAllJoinedCarPools();
            return Request.CreateResponse(HttpStatusCode.OK, carPools);
        }

        [HttpPost]
        [Route("api/carpoolopportunity/get-user-joined-car-pools-opportunities")]
        public HttpResponseMessage GetUserJoinedCarPoolsOpportunities(object userId)
        {      
            var carPools = _joinedCarPoolsOpportunityRepository.GetAllUserJoinedCarPools(Guid.Parse(userId.ToString()));
            return Request.CreateResponse(HttpStatusCode.OK, carPools);
        }        

        [HttpPost]
        [Route("api/carpoolopportunity/leave")]
        public HttpResponseMessage Delete(JoinCarPoolsOpportunityRequest joinCarPoolsOpportunityRequest)
        {
            if (ModelState.IsValid)
            {
                CarPoolOpportunity carPool = GetCarPoolOpportunityById(joinCarPoolsOpportunityRequest);

                var carPoolOpportunity = _joinedCarPoolsOpportunityRepository
                    .GetBy(joinCarPoolsOpportunityRequest.UserId, carPool.CarPoolId);

                carPool.AvailableSeats += 1;
                _joinedCarPoolsOpportunityRepository.DeleteJoinedCarPoolOpportunity(carPoolOpportunity);
                Update(carPool);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        private List<CarPoolsOpportunityRequest> GetCarPools()
        {
            var users = _userRepository.GetAllUsers();
            var carPools = _mappingEngine.Map<List<CarPoolsOpportunityRequest>>(_carPoolOpportunityRepository.GetAllCarPools());
            foreach (var carPool in carPools)
            {
                carPool.UserId = carPool.OwnerOrLeader;
                carPool.OwnerOrLeader = users.FirstOrDefault(x => x.UserId == Guid.Parse(carPool.OwnerOrLeader)).Name;
            }
            return carPools;
        }
        private static bool? PeriodsOverLap(CarPoolOpportunity carPool, List<CarPoolsOpportunityRequest> carPools)
        {
            return carPools?.Any(x => x.DepartureTime < carPool.ExpectedArrivalTime && x.ExpectedArrivalTime < carPool.DepartureTime);
        }
        private List<CarPoolsOpportunityRequest> SetUp(JoinCarPoolsOpportunityRequest joinCarPoolsOpportunityRequest, bool isCreate)
        {
            var carPools = GetCarPools();
            if (isCreate)
            {
                joinCarPoolsOpportunityRequest.JoinCarPoolsOpportunityId = Guid.NewGuid();
                joinCarPoolsOpportunityRequest.DateJoined = DateTime.Now;
            }
            return carPools;
        }
        private JoinCarPoolsOpportunity Map(JoinCarPoolsOpportunityRequest joinCarPoolsOpportunityRequest)
        {
            return _mappingEngine.Map<JoinCarPoolsOpportunity>(joinCarPoolsOpportunityRequest);
        }
        private CarPoolOpportunity GetCarPoolOpportunityById(JoinCarPoolsOpportunityRequest joinCarPoolsOpportunityRequest)
        {
            return _carPoolOpportunityRepository.GetById(joinCarPoolsOpportunityRequest.CarPoolId);
        }
        private void Update(CarPoolOpportunity carPool)
        {
            _carPoolOpportunityRepository.Update(carPool);
        }
    }
}