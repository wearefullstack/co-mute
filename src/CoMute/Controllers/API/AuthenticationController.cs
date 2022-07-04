using Mutestore.Models;
using CoMute.Web.Interface;
using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
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
        private IUserRepository _userRepository;
      
        public AuthenticationController()
        {
           
            this._userRepository = new UserRepository(new DatabaseContext());
        }
        [Route("api/Authentication")]
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            var user = _userRepository.Login(loginRequest.Email,loginRequest.Password);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, loginRequest);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
