using MvCCarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvCCarPool.Controllers
{
    public class CreateCarPoolUserController : Controller
    {
        CarPoolEntities1 carPoolData = new CarPoolEntities1();
        // GET: CreateCarPoolUser
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: CreateCarPoolUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CreateCarPoolUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreateCarPoolUser/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] CarPoolUser collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (!ModelState.IsValid)

                    return View();
                carPoolData.CarPoolUsers.Add(collection);

                carPoolData.SaveChanges();

                return RedirectToAction("Index", "CarPoolUserList");
            }
            catch
            {
                return View();
            }
        }

        // GET: CreateCarPoolUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CreateCarPoolUser/Edit/5
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

        // GET: CreateCarPoolUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CreateCarPoolUser/Delete/5
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
