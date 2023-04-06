using CoMute.Web.Models.DAL;
using CoMute.Web.Models.Dto;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IHttpActionResult PostLoginUser(LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                bool verified = DAL.checkPassword(loginRequest.Email, loginRequest.Password);

                if (!verified)
                {
                    return BadRequest("Invalid Email or Password");
                }
                else
                {
                    return Ok(loginRequest.Email);
                }
            }
            else
            {
                return BadRequest("Invalid Entry");
            }
        }
    }
}
