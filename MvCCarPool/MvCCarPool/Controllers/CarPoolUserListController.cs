using MvCCarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvCCarPool.Controllers
{
    public class CarPoolUserListController : Controller
    {
        CarPoolEntities1 cbe = new CarPoolEntities1();
        // GET: CarPoolUserList
        public ActionResult Index()
        {

            var items = cbe.CarPoolUserLists;
            
            return View(items.ToList());
        }

        // GET: CarPoolUserList/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarPoolUserList/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(cbe.CarPoolUsers, "Id", "FName");
            ViewBag.CarPoolId = new SelectList(cbe.CarPoolLists, "CarPoolId", "Owner");
            return View();
           
        }

        // POST: CarPoolUserList/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Idx,UserId,CarPoolId")] CarPoolUserList collection)
        {
            CarPoolEntities1 c = new CarPoolEntities1();
            try
            {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    c.CarPoolUserLists.Add(collection);
                    c.SaveChanges();
                    
                }
                
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: CarPoolUserList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarPoolUserList/Edit/5
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

        // GET: CarPoolUserList/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarPoolUserList/Delete/5
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
