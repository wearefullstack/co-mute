using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult Logout()
        {
            

            // set the IsLoggedIn flag to false
            Session["IsLoggedIn"] = false;

            // redirect the user to the login page
            return RedirectToAction("Index", "Account");
        }
    }
}