using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
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
        [Route("user/add")]
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            var user = new User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress
            };

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }
    }
}
