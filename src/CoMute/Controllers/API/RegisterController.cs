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
    public class RegisterController : ApiController
    {
        //[Route("user/add")]
        //Adds user into the database and saves the changes to thte database
        //The Post comes from the "register.js" file In the Conent Folder under the js folder
        //The DB model is found in the Model folder, under the DAL foler.
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            var user = new UsersList()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress,
                PhoneNumber = registrationRequest.PhoneNumber,
                Password = registrationRequest.Password
            };



            ComuteDBEntities db = new ComuteDBEntities();
            db.UsersLists.Add(user);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }
    }
}