using CoMute.Web.Models.Dto;
using System.Linq;
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
            var context = new CoMuteEntities();

            try
            {
                var recored = context.Users.FirstOrDefault(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password);
                if (recored is null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Request.CreateResponse(HttpStatusCode.OK, recored);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, loginRequest);
            }
        }
    }
}
