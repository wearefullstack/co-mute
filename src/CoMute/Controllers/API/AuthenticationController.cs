using CoMute.Web.Models.Dto;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
