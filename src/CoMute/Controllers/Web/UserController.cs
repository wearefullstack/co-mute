using Mutestore.Models;
using CoMute.Web.Interface;
using CoMute.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        public UserController()
        {
            this._userRepository = new UserRepository(new DatabaseContext());
        }
        // GET: User
        public ActionResult Profile()
        {
            if (string.IsNullOrEmpty(Session["email"] as string))
            {
                return RedirectToAction("Index", "Home");
            }
            string email = Session["email"].ToString();
            var userProfile = _userRepository.GetUserByEmail(email);
            return View(userProfile);
        }
    }
}