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
        //Takes the loginRequest data and then check to see if the user exists and if the password exists. 
        //It returns a OK if the password and Email combo words together. It returns a Not Found if the User doesn't Exist or Password is incorrect.
        //The Post comes action comes from the "login.js" file in the conent folder under the js folder. The Return response is also sent back there.
        //The DB Model is found under the Model folder in the DAL folder.
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
