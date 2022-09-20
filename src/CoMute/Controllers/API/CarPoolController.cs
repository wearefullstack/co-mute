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
    public class CarPoolController : ApiController
    {
        private readonly CarPoolService _carPoolService;

        public CarPoolController()
        {
            _carPoolService = new CarPoolService();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(CreateCarPoolRequest request)
        {
            await _carPoolService.addCarPool(request);
           return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
