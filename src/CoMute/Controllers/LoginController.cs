using CoMute.Web.Models;
using CoMute.Web.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginProcessor(User user) 
        {
            SecurityService securityService = new SecurityService();
            if(securityService.IsValid(user))
            {
                return View("LoginSuccess", user);
            }
            else
            {
                return View("LoginFailure", user);
            }
           
        }
    }
}