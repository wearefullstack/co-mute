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
    public class UserController : ApiController
    {
        //[Route("user/add")]
        [HttpPost]
        public IHttpActionResult PostNewUser(RegistrationRequest registrationRequest)
        {
            if (ModelState.IsValid) {
                DAL.RegisterUser(registrationRequest.Password, registrationRequest.EmailAddress, registrationRequest.Name, registrationRequest.Surname, registrationRequest.PhoneNumber);
            var user = new User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress
            };

                return Ok();
            }
            else
            {
                return BadRequest("Invalid data."); ;
            }
        }

        [HttpGet]
        public IHttpActionResult GetUser(string email)
        {
            if(email == null)
            {
                return BadRequest("Email Empty.");
            }
            else {
                DAL.getUser(email);
                return Ok();
            }
        }

        public void PutUpdateUser(int id, [FromBody] string value)
        {
        }
    }
}
