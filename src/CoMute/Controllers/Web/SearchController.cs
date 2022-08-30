using System.Web.Mvc;
using CoMute.Lib.Dto;
using CoMute.Lib.services;

namespace CoMute.Web.Controllers.Web
{
    public class SearchController : Controller
    {
        private readonly IUserService _service;

        public SearchController(IUserService service)
        {
            _service = service;
        }

        private UserDto GetMe() => Session["me"] as UserDto;

        public ActionResult Index()
        {
            var me = GetMe();

            if (me == null)
                return RedirectToAction("login", "Home");

            return View();
        }
    }
}