using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLibrary;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        [Route("user/add")]
        public HttpResponseMessage Post(RegistrationRequest registrationRequest) // Register New User
        {
            var user = new DataLibrary.User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress,
                PhoneNumber = registrationRequest.PhoneNumber,
                Password = registrationRequest.Password
            };

            using (FullStackTestEntities entities = new FullStackTestEntities()) // Save New User
            {
                entities.Users.Add(user);
                entities.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }
    }
}