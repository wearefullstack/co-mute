using Mutestore.Models;
using CoMute.Web.Interface;
using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {

        private ICarPoolRepository _carPoolRepository;
        public HomeController()
        {
            this._carPoolRepository = new CarPoolRepository(new DatabaseContext());
        }

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
        public ActionResult MainPool(string email)
        {
            Session["email"] = email;
            var carPool = _carPoolRepository.GetCarPools();
            return View(carPool);
        }
        public ActionResult CreatePool()
        {
            return View();
        }
        public ActionResult Search()
        {
            var carPool = _carPoolRepository.GetCarPools();
            return View(carPool);
        }
    }
}