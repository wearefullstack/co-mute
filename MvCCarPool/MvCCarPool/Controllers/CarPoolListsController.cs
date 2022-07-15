using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvCCarPool.Models;

namespace MvCCarPool.Controllers
{
    public class CarPoolListsController : Controller
    {
        private CarPoolEntities1 db = new CarPoolEntities1();

        // GET: CarPoolLists
        public ActionResult Index()
        {
            var carPoolLists = db.CarPoolLists.Include(c => c.CarPoolUser);
            return View(carPoolLists.ToList());
        }

        // GET: CarPoolLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarPoolList carPoolList = db.CarPoolLists.Find(id);
            if (carPoolList == null)
            {
                return HttpNotFound();
            }
            return View(carPoolList);
        }

        // GET: CarPoolLists/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.CarPoolUsers, "Id", "FName");
            return View();
        }

        // POST: CarPoolLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Destination,ArrivalTime,DepartureTime,Origin,AvailableSeats,Owner,Notes,CarPoolId,Id")] CarPoolList carPoolList)
        {
            if (ModelState.IsValid)
            {
                db.CarPoolLists.Add(carPoolList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.CarPoolUsers, "Id", "FName", carPoolList.Id);
            return View(carPoolList);
        }

        // GET: CarPoolLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarPoolList carPoolList = db.CarPoolLists.Find(id);
            if (carPoolList == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.CarPoolUsers, "Id", "FName", carPoolList.Id);
            return View(carPoolList);
        }

        // POST: CarPoolLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Destination,ArrivalTime,DepartureTime,Origin,AvailableSeats,Owner,Notes,CarPoolId,Id")] CarPoolList carPoolList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carPoolList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.CarPoolUsers, "Id", "FName", carPoolList.Id);
            return View(carPoolList);
        }

        // GET: CarPoolLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarPoolList carPoolList = db.CarPoolLists.Find(id);
            if (carPoolList == null)
            {
                return HttpNotFound();
            }
            return View(carPoolList);
        }

        // POST: CarPoolLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarPoolList carPoolList = db.CarPoolLists.Find(id);
            db.CarPoolLists.Remove(carPoolList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
