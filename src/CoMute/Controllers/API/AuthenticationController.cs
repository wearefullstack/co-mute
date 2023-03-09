using CoMute.Web.Models;
using CoMute.Web.Models.DAL;
using CoMute.Web.Models.Dto;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            ComuteDBEntities db = new ComuteDBEntities();
            UsersList user = db.UsersLists.FirstOrDefault(x=>x.EmailAddress ==loginRequest.Email && x.Password ==loginRequest.Password);

            if(user != null && user.UserID!=0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            
        }
    }
}
