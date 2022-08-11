using CoMute.Core.Interfaces.Repositories;
using CoMute.Web.Models.Dto;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>      
        [HttpPost]
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            if (!string.IsNullOrEmpty(loginRequest.Email))
            {
                var user = _userRepository.ValidateUser(loginRequest.Email, loginRequest.Password);
    
                if (user != null)
                {   
                    FormsAuthentication.SetAuthCookie(user.UserId.ToString(), false);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }                    
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
