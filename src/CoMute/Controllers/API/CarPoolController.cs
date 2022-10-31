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

        [Route("api/user/{id}/carpool/memberships")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCarPoolMemberships(int id)
        {
            User user = await _comuteContext.Users.FindAsync(id);
            if (user != null)
            {
                _comuteContext.Entry(user).Collection(s => s.CarPoolMemberships).Load();
                if (user.CarPoolMemberships.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user.CarPoolMemberships);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
