using CoMute.Web.Data;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    //------------------------------------------- UserController (Api) : Amber Bruil ---------------------------------------------------------//
    public class UserController : ApiController
    {
        dbCoMuteEntities db = new dbCoMuteEntities();

        /// <summary>
        /// Http response POST
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adding user to database
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(RegistrationRequest registrationRequest)
        {
            var user = new tblRegister
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                Email = registrationRequest.EmailAddress,
                Phone = registrationRequest.PhoneNumber,
                Password = registrationRequest.Password,
            };
            db.tblRegisters.Add(user);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }
    }
    //--------------------------------------------------- 0o00ooo End of File ooo00o0 --------------------------------------------------------//
}
