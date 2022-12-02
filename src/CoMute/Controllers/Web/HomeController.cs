using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public ActionResult UserIndex()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginRequest _user)
        {
            return RedirectToAction("login", "user", _user);
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(User _user)
        {
            return RedirectToAction("register", "user",_user);
        }

        
    }
}