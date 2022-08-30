using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoMute.Lib.Dto;
using CoMute.Lib.services;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        private readonly IUserService _service;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionname = filterContext.ActionDescriptor.ActionName.ToLower();

            if (GetMe() == null && actionname != "login" && actionname != "register" && actionname != "about")
            {
                filterContext.Result = RedirectToAction("login");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }

        public HomeController(IUserService service)
        {
            _service = service;
        }

        private UserDto GetMe()
        {
            var me = Session["me"] as UserDto;

            //if (me == null && Dns.GetHostName() == "soft")
            //    Session["me"] = me = _service.GetLogin("wumi.inyang@ansyl.com", "abcd1234");

            return me;
        }

        public ActionResult Index()
        {
            var me = GetMe();

            if (me == null)
                return RedirectToAction("login");
            else
                return RedirectToAction("DashBoard");
        }

        public ActionResult Login()
        {
            return View(new LoginRequest());
        }

        public ActionResult DashBoard()
        {
            UserDto me = GetMe();

            var model = _service.GetUserPageModel(me.UserId);

            return View(model);
            //return GetMe().Email;
        }

        [HttpPost]
        public ActionResult Login(LoginRequest model)
        {
            try
            {
                var user = _service.GetLogin(model.Email, model.Password);

                Session.Add("me", user);

                return RedirectToAction("DashBoard");
            }
            catch (Exception e)
            {
                TempData.Add("Error", e.Message);
                return View(model);
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            var model = _service.GetUserPageModel(UserId);
            return View(model);
        }

        [HttpPost]
        public ActionResult MyProfilePost(UserDto dto)
        {
            Session["me"] = _service.UpdateProfile(UserId, dto);
            return RedirectToAction("DashBoard");
        }

        public ActionResult JoinCarPool(int id)
        {
            try
            {
                _service.JoinPool(UserId, id);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction("DashBoard");
        }

        private int UserId => GetMe().UserId;

        public ActionResult LeaveCarPool(int id)
        {
            _service.LeavePool(UserId, id);

            return RedirectToAction("DashBoard");
        }

        public ActionResult RegisterCarPool()
        {
            var dto = new PoolDto();

            if (TempData.ContainsKey("dto"))
            {
                dto = TempData["dto"] as PoolDto;
            }
            //else if (Dns.GetHostName() == "soft")
            //{
            //    dto.DepartTime = "11:00";
            //    dto.ArriveTime = "12:00";
            //    dto.Origin = "Cape Town";
            //    dto.Destination = "Mowbray";
            //    dto.AvailableSeats = 5;
            //    dto.OwnerId = GetMe().UserId;
            //    dto.AvailableDays = "Monday,Friday".Split(',');
            //    dto.CreatedTime = DateTime.Now;
            //}

            return View(dto);
        }

        [HttpPost]
        public ActionResult RegisterCarPool(PoolDto dto)
        {
            try
            {
                dto.OwnerId = GetMe().UserId;
                _service.CreatePool(dto);
                return RedirectToAction("DashBoard");
            }
            catch (Exception e)
            {
                TempData["dto"] = dto;
                TempData["error"] = e.Message;
                return RedirectToAction("RegisterCarPool");
            }
        }

        public ActionResult Owners(int? id)
        {
            var model = _service.GetUserPageModel(id ?? GetMe().UserId);

            return View(model);
        }

        /// <summary>
        /// pools owned by user ID (or current user)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OwnedPools(int? id)
        {
            var model = _service.GetUserPageModel(id ?? GetMe().UserId);

            return View(model);
        }
    }
}