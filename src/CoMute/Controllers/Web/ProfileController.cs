using CoMute.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class ProfileController : Controller
    {
        private readonly UserService _userService;

        public ProfileController()
        {
            _userService = new UserService();
        }

        // GET: Profile
      
        public ActionResult Index()
        {
            var name = User.Identity.Name;

            return View();
        }
    }
}