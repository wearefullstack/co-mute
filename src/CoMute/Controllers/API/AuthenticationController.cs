using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using CoMute.Web.App_Start;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        private readonly UserManager<IdentityModel> _userManager;
        private readonly SignInManager<IdentityModel, string> _signInManager;

        public AuthenticationController()
        {
            //_userManager = new UserManager<User>(IUserStore<User>);
            //_signInManager = signInManager;
        }

        //public async Task<HttpResponseMessage> Post(LoginRequest loginRequest)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //if (_userManager.Users == null)
        //        //{
        //        //    foreach (var _user in _users) { _userManager.Users.Append<IdentityModel>(_user); }
        //        //}
        //        //IdentityModel user = await _userManager.FindByNameAsync(loginRequest.Email);
        //        //if (user != null)
        //        //{
        //        //    var result = _signInManager.PasswordSignInAsync(user.UserName,
        //        //        loginRequest.Password, false, false);
        //        //    if (result.IsCompleted)
        //        //    {
        //        //        return Request.CreateResponse(HttpStatusCode.OK);
        //        //    }
        //        //}

        //    }
        //    return Request.CreateResponse(HttpStatusCode.NotFound);
        //}


    }
}
