
using CoMute.Web.Data;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.API
{

    //------------------------------------------- RegistrationController (Api) : Amber Bruil ---------------------------------------------------------//
    public class RegistrationController : ApiController
    {

        dbCoMuteEntities db = new dbCoMuteEntities();

        #region CRUD
        /// <summary>
        /// Method to inserting the user into the register database
        /// </summary>
        /// <param name="registrationRequest"></param>
        /// <returns></returns>
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
        #endregion CRUD
    }
    //--------------------------------------------------- 0o00ooo End of File ooo00o0 --------------------------------------------------------//
}