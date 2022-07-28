using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{

    [Authorize]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Profile()
        {
            int userId = Int32.Parse(User.Identity.Name);

            var adapter = new Adapter.Adapter(new HttpClient());
            var res = await adapter.GetUserProfileById(userId);

            return View(res);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProfile(ProfileViewModel model)
        {
            var adapter = new Adapter.Adapter(new HttpClient());
            var res = await adapter.UpdateProfileAccount(model);
            return Json(res);

        }
    }
}