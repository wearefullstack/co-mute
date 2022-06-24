using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        public ActionResult Index() // Landing page
        {
            return View();
        }

       public ActionResult MyProfile() // Redirect to Profile page for viewing and updating records, delete functionality not requested
        {
            ViewBag.Message = "Update your profile details here.";
            return View();
        }

        public ActionResult Register() // Register New User
        {
            ViewBag.Message = "Register new account.";
            return View();
        }
       public ActionResult Search() // View Rides to join, 
        {
            ViewBag.Message = "View transport opportunities here.";
            return View();
        }

        public ActionResult About() // Unchanged
        {
            ViewBag.Message = "About us here.";
            return View();
        }
    }
}