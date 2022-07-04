using Mutestore.Models;
using CoMute.Web.Interface;
using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
        private ICarPoolRepository _carPoolRepository;
        public CarPoolController()
        {
            this._carPoolRepository = new CarPoolRepository(new DatabaseContext());
        }
        // GET: CarPool
        [Route("api/pool/post")]
        public HttpResponseMessage Post(CarPool carPool)
        {
            _carPoolRepository.InsertCarPool(carPool);
            _carPoolRepository.Save();
            return Request.CreateResponse(HttpStatusCode.Created, carPool);
        }
    }
}