using CoMute.Web.Data;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
        private readonly ComuteContext _comuteContext;

        public CarPoolController()
        {
            _comuteContext = new ComuteContext();
        }

        [Route("api/carpool/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            CarPool carpool = await _comuteContext.CarPools.FindAsync(id);
            if(carpool != null)
            {
                _comuteContext.Entry(carpool).Collection(s => s.AvailableDays).Load();
                CarPoolDto carPoolDto = new CarPoolDto
                {
                    Id = carpool.Id,
                    UserId = carpool.UserId,
                    AvailableDays = carpool.AvailableDays,
                    AvailableSeats = carpool.AvailableSeats,
                    DepartureTime = carpool.DepartureTime,
                    ExpectedArrivalTime = carpool.ExpectedArrivalTime,
                    Origin = carpool.Origin,
                    Destination = carpool.Destination,
                    Notes = carpool.Notes
                };
                return Request.CreateResponse(HttpStatusCode.OK, carPoolDto);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
