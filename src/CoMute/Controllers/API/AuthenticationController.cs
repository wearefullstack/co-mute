using CoMute.Web.Models.Dto;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLibrary;
using System.Linq;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        public HttpResponseMessage Post(LoginRequest loginRequest) // User Authentication through DB, better authentication methods available
        {
            using (FullStackTestEntities entities = new FullStackTestEntities())
            {
                var user = entities.Users.FirstOrDefault(x => x.Password == loginRequest.Password && x.EmailAddress == loginRequest.Email);

                if (user != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,user);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }         
        }
    }
}
