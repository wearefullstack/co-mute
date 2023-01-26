using CoMute.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.API
{
    
    public class CarPoolController : Controller
    {
        dbCoMuteEntities db = new dbCoMuteEntities();

        /// <summary>
        /// Http response to display view: user's car pools
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }
    }
}