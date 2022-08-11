using System.Web.Mvc;
using System.Web.Security;

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

        public void LogOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();  
        }
    }
}