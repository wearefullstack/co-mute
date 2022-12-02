using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.App_Start;
using CoMute.Web.Data;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CoMute.Web.Controllers.API
{
    [Authorize]
    public class UserController : Controller
    {
        #region private Properties
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly RepWrapper2 repo;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion private Properties

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, RepWrapper2 _repo)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this.repo = _repo;
        }

        #region public Properties
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion public Properties

        #region CRUD
        [HttpGet]
        public ActionResult MyProfile()
        {
            User user = getCurrentUser();
            return View(user);
        }

        [HttpPost]
        public ActionResult MyProfile(User user)
        {
            User user1 = getCurrentUser();
            repo.AppUsers.Update(user, user1);
            return RedirectToAction("Index", "Carpool");
        }

        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("index", "Home", model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index","Carpool");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return RedirectToAction("index", "Home", model);
            }
        }
        //[HttpPost]
        //[Route("user/add")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(User model)
        { 
            if (ModelState.IsValid)
            {
                var user = new IdentityModel { UserName = model.Name + "_" + model.Surname , Email = model.EmailAddress };
                
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    var appUser = populateUser(model);
                    string sTest = getUserID(user.UserName);
                    appUser.UserID = sTest;
                    repo.AppUsers.Create(appUser);
                    repo.Save();
                    return RedirectToAction("Index", "Home");
                }
              
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion CRUD

        #region Private Helpers

        private User populateUser(User model)
        {
            return new User { Name = model.Name, Surname = model.Surname, EmailAddress = model.EmailAddress, PhoneNumber = model.PhoneNumber, UserID = getUserID(model.Name + "_" + model.Surname), Password = "##" };
        }

        private User getCurrentUser()
        {
            string sCondition = User.Identity.GetUserId();
            User[] arrUsers = repo.AppUsers.FindAll().ToArray();
            foreach (User uCurrent in arrUsers)
            {
                if (uCurrent.UserID == sCondition)
                {
                    return uCurrent;
                }
            }
            return new User();
        }

        private User getUser(string id)
        {
            foreach(var user in repo.AppUsers.FindAll()) 
            {
                if (user.UserID == id)
                {
                    return user;
                }
            }

            return null;
        }

        private string getUserID(string username)
        {
            string s = User.Identity.GetUserId();
            if (s == "")
            {
                return string.Empty;
            }
            return SignInManager.UserManager.Users.First<IdentityModel>(u => u.UserName == username).Id;
        }

        #endregion Private Helpers
    }
}
    