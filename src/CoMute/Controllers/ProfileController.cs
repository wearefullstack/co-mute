using CoMute.Web.Data;
using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Profile
        public ActionResult Profile()
        {
            int userId = Convert.ToInt32(Session["Id"]);
            if (userId==0)//user not logged in
            {
                return RedirectToAction("Login", "User");
            }
            return View(db.Users.Find(userId));//user logged in
        }
        //POST: UPDATE PROFILE
        //[HttpPost]
        //public ActionResult UpdateProfile(Profile obj)
        //{
        //    int userId = Convert.ToInt32(Session["Id"]);
        //}

    }
}