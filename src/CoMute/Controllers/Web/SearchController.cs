using CoMute.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class SearchController : Controller
    {
        private readonly CarPoolService _carPoolService;

        public SearchController()
        {
            _carPoolService = new CarPoolService();
        }
       
        // GET: Search
        public ActionResult Index()
        {
            return View(_carPoolService.GetAllCarPools());
        }

        [HttpPost]
        public ActionResult Index(string keyword)
        {
            return View(_carPoolService.SearchCarPools(keyword));
        }

    }
}