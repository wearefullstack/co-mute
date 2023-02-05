using CoMute.Web.Data;
using CoMute.Web.Models.Dto;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    //------------------------------------------- AuthenticationController (Api) : Amber Bruil ---------------------------------------------------------//
    public class AuthenticationController : ApiController
    {
        dbCoMuteEntities db = new dbCoMuteEntities();

        /// <summary>
        /// Respond to Http request when loggin in user
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            var user = db.tblRegisters.Where(zz => zz.Email == loginRequest.Email && zz.Password == loginRequest.Password).FirstOrDefault();

            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }


        }
    }
    //--------------------------------------------------- 0o00ooo End of File ooo00o0 --------------------------------------------------------//
}
