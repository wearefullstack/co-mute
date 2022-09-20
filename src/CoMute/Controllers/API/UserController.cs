using CoMute.Web.Models;
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

    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(RegistrationRequest request)
        {
            var registerUser = await _userService.RegisterNewUser(request);

            if (registerUser)
                return Request.CreateResponse(HttpStatusCode.Created, new User { Name = request.Name, Surname = request.Surname, Email = request.EmailAddress });
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        
        [HttpPost,Route("updateprofile")]
        public async Task<HttpResponseMessage> UpdateProfile(Profile profile )
        {
            await _userService.UpdateUserProfile(profile);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage GetUserProfile()
        {
            var useName = User.Identity.Name;
            var profile = _userService.GetUserProfileByEmail(useName);

            if (profile == null) return Request.CreateResponse(HttpStatusCode.BadRequest);
            else return Request.CreateResponse(HttpStatusCode.OK, profile);
        }
    }
}
