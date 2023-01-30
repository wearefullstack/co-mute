using CoMute.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.API
{
    public class CarpoolsController : Controller
    {

        dbCoMuteEntities db = new dbCoMuteEntities();


        /// <summary>
        /// Http response to display view: user's car pools
        /// </summary>
        /// <returns></returns>
       // [Route("carpool")]
        public ActionResult Index()
        {
            return View(db.tblUserCarPools.ToList());
        }


        public ActionResult AddCarpool()
        {
            return View();
        }

        /// <summary>
        /// Method to add a carpool to the database
        /// </summary>
        /// <param name="carpoolToCreate"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddCarPool([Bind(Exclude = "CarPoolID")] tblUserCarPool carpoolToCreate)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            db.tblUserCarPools.Add(carpoolToCreate);

            db.SaveChanges();

            return RedirectToAction("Index", "Carpools");
        }
    }
}