using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CoMute.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AcceptVerbsAttribute = System.Web.Mvc.AcceptVerbsAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
        private CarPoolRepository _carPoolRepository = new CarPoolRepository();

        [Route("api/addcarpool")]
        public HttpResponseMessage Post(CreateCarPoolRequest createCarPool)
        {
            createCarPool.CarPoolGuid = Guid.NewGuid();

            var carPool = new CarPool()
            {
                CarPoolGuid = Guid.NewGuid(),
                Origin = createCarPool.Origin,
                Destination = createCarPool.Destination,
                DepartureTime = createCarPool.DepartureTime,
                ExpectedArrivalTime = createCarPool.ExpectedArrivalTime,
                AvailableSeats = createCarPool.AvailableSeats,
                Notes = createCarPool.Notes,
                CreatedDate = DateTime.Now,
                UserGuid = createCarPool.UserGuid 
            };

            _carPoolRepository.AddCarpool(carPool);
             
            return Request.CreateResponse(HttpStatusCode.Created, createCarPool);
        }
       
        [Route("api/GetCarPools")] 
        public List<CarPool> GetCarPools()
        {
            AddingResponse response = new AddingResponse();

            var carpools = _carPoolRepository.GetCarpools();

            return carpools; //Request.CreateResponse(HttpStatusCode.Created, carpools);
        }

        [Route("api/carpool/GetAvailableCarpools/{userGuid}")]
        public List<CarPool> SearchCarPools(string userGuid)
        {
            AddingResponse response = new AddingResponse();

            var carpools = _carPoolRepository.GetAvailableCarpools(Guid.Parse(userGuid));

            return carpools; //Request.CreateResponse(HttpStatusCode.Created, carpools);
        }

        [Route("api/carpool/join/{carpoolGuid}/{userGuid}")]
        public HttpResponseMessage JoinCarPools(string carpoolGuid,string userGuid)
        {
            AddingResponse response = new AddingResponse();

            var carpools = _carPoolRepository.JoinPool(Guid.Parse(carpoolGuid), Guid.Parse(userGuid));

            return Request.CreateResponse(HttpStatusCode.Created, carpoolGuid);
        }

        [Route("api/carpool/leave/{carpoolGuid}/{userGuid}")]
        public HttpResponseMessage LeaveCarPools(string carpoolGuid, string userGuid)
        {
            AddingResponse response = new AddingResponse();

            var carpools = _carPoolRepository.LeaveCarPool(Guid.Parse(carpoolGuid), Guid.Parse(userGuid));

            return Request.CreateResponse(HttpStatusCode.Created, carpoolGuid);
        }

        [Route("api/carpool/GetJoinedCarpools/{userGuid}")]
        public List<CarPoolJoinedByMember> CarPoolJoinedByMember(string userGuid)
        {
            AddingResponse response = new AddingResponse();

            var carpools = _carPoolRepository.GetJoinedCarPool(Guid.Parse(userGuid));

            return carpools;
        }
    }
}