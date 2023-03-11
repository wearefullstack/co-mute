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
            ComuteDBEntities db = new ComuteDBEntities();
            UsersList user = db.UsersLists.FirstOrDefault(x=>x.UserID == LoggedInUser.Id);
            if (user != null && user.UserID != 0)
            {
                try
                {
                    user.Name = registrationRequest.Name;
                    user.Surname = registrationRequest.Surname;
                    user.PhoneNumber = registrationRequest.PhoneNumber;
                    user.EmailAddress = registrationRequest.EmailAddress;
                    user.Password = registrationRequest.Password;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}