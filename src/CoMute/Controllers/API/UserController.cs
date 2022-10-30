using CoMute.Web.Data;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        private readonly ComuteContext _comuteContext;

        public UserController()
        {
            _comuteContext = new ComuteContext();
        }

        [Route("api/user/add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post(RegistrationRequest registrationRequest)
        {
            var user = new User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress,
                PhoneNumber = registrationRequest.PhoneNumber,
                Password = registrationRequest.Password
            };

            _comuteContext.Users.Add(user);
            await _comuteContext.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }
    }
}
