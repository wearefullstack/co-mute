using Mutestore.Models;
using CoMute.Web.Interface;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        private IUserRepository _userRepository;
        public UserController()
        {
            this._userRepository = new UserRepository(new DatabaseContext());
        }
        [Route("api/user")]
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            _userRepository.InsertUser(registrationRequest);
            _userRepository.Save();
            return Request.CreateResponse(HttpStatusCode.Created, registrationRequest);
        }
        [Route("api/user/update")]
        public HttpResponseMessage Update(RegistrationRequest registrationRequest)
        {
            _userRepository.UpdateUser(registrationRequest);
            _userRepository.Save();
            return Request.CreateResponse(HttpStatusCode.Created, registrationRequest);
        }
        [Route("api/user/get")]
        public HttpResponseMessage Get(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            return Request.CreateResponse(HttpStatusCode.Created, user);
        }

    }
}
