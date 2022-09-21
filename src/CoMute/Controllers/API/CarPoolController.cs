using CoMute.Web.Models.Dto;
using CoMute.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    [RoutePrefix("api/carpool")]
    public class CarPoolController : ApiController
    {
        private readonly CarPoolService _carPoolService;

        public CarPoolController()
        {
            _carPoolService = new CarPoolService();
        }

        [HttpPost,Authorize]
        public async Task<HttpResponseMessage> Post(CreateCarPoolRequest request)
        {
            request.Owner_Leader = User.Identity.Name;
           await _carPoolService.addCarPool(request);
           return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpGet]
        public HttpResponseMessage GetUserProfile()
        {
            var useName = User.Identity.Name;
            return Request.CreateResponse(HttpStatusCode.OK, _carPoolService.GetCarPool(useName));
        }
        
        [HttpGet,Route("GetAll")]
        public HttpResponseMessage GetAllCarPools()
        {
            
            return Request.CreateResponse(HttpStatusCode.OK, _carPoolService.GetAllCarPools());
        }
        
        [HttpPost,Route("Search")]
        public HttpResponseMessage SearchCarPools(string keyword)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _carPoolService.SearchCarPools(keyword));
        }
        
        [HttpPost,Route("Join")]
        public async Task<HttpResponseMessage> JoinCarPool(int carpoolId)
        {
            var useName = User.Identity.Name;
            await _carPoolService.JoinCarPool(carpoolId, useName);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
