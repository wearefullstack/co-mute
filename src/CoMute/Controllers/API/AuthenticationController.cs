using CoMute.Web.Data;
using CoMute.Web.Models.Dto;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.API
{

   
    public class AuthenticationController : ApiController
    {


        CarPoolEntities db = new CarPoolEntities();
        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
 
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            var user = db.Registers.Where(zz => zz.Email == loginRequest.Email && zz.Password == loginRequest.Password).FirstOrDefault();
            
            if (user == null)
            {
                /*Session.Add("searchStr", user.Register_ID);*/
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            { 
                
                return Request.CreateResponse(HttpStatusCode.OK); 
            }

            
        }
    }
}
