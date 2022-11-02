using CoMute.Web.Data;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        private readonly ComuteContext _comuteContext;

        public AuthenticationController()
        {
            _comuteContext = new ComuteContext();
        }

        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
        [Route("api/authentication")]
        [HttpPost]
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            bool IsValidUser = _comuteContext.Users
                .Any(user => user.EmailAddress.ToLower() == loginRequest.Email.ToLower() && user.Password == loginRequest.Password);

            if (IsValidUser)
            {
                User user =_comuteContext.Users.First(s => s.EmailAddress.ToLower() == loginRequest.Email.ToLower() && s.Password == loginRequest.Password);
                UserDto userDto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    EmailAddress = user.EmailAddress

                };
                return Request.CreateResponse(HttpStatusCode.OK, userDto);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}
