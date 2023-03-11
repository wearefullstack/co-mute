using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using CoMute.Web.Models;
using CoMute.Web.Models.DAL;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.Controllers.Web
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            ComuteDBEntities db = new ComuteDBEntities();
            int idNum = 0;
            idNum = LoggedInUser.Id;

            if (idNum == 0)
            {
               return RedirectToAction("Index", "Home");
            }

            UsersList user = db.UsersLists.FirstOrDefault(x => x.UserID == idNum);

            return View(user);
        }
    }
}