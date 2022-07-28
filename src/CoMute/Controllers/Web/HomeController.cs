using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CoMute.Web.Controllers.Web
{

    public class HomeController : Controller
    {
        //[Route("Account/Login")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest login)
        {
            var adapter = new Adapter.Adapter(new HttpClient());
            var user = await adapter.Login(login);

            if (user > 0)
            {
                FormsAuthentication.SetAuthCookie(user.ToString(), true);
                return RedirectToAction("Index", "CarPool");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationRequest model)
        {
            return View();
        }
    }
}