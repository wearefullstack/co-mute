using CoMute.Web.Models;
using CoMute.Web.Models.DAL;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class ProfileController : ApiController
    {
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}