using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CoMute.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class CarPoolController : Controller
    {
        private CarPoolRepository _carPoolRepository = new CarPoolRepository();

        // GET: CarPool
        public ActionResult Index()
        {
            return View();
        }

        // GET: CarPool/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarPool/Create
        public ActionResult Create()
        {
           
            return View();
        }

        public ActionResult CarPoolSearch()
        {
           var carPoolList = _carPoolRepository.GetCarpools();
            return View(carPoolList);
        }

        public ActionResult AvailableCarPools()
        {
            List<CarPool> carPoolList = new List<CarPool>();
            carPoolList = _carPoolRepository.GetCarpools();

            return View(carPoolList);
        }

        public ActionResult SearchCarPools()
        {


            return View();
        }

        public ActionResult JoinedCarPools()
        {


            return View();
        }

        // POST: CarPool/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CarPool/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarPool/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CarPool/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
