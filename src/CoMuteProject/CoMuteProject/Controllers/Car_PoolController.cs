using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoMuteProject.Models;

namespace CoMuteProject.Controllers
{
    [Authorize]
    public class Car_PoolController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Car_Pool
        public ActionResult Index()
        {
            return View(db.car_Pools.ToList());
        }

        // GET: Car_Pool/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Pool car_Pool = db.car_Pools.Find(id);
            if (car_Pool == null)
            {
                return HttpNotFound();
            }
            return View(car_Pool);
        }

        // GET: Car_Pool/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car_Pool/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Departure_Time,Price,Date_Created,Arrival_Time,Origin,Days_Available,Destination,Available_Seats,Owner,Notes")] Car_Pool car_Pool)
        {
            if (ModelState.IsValid)
            {
                car_Pool.Date_Created = DateTime.Now;
                db.car_Pools.Add(car_Pool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car_Pool);
        }

        // GET: Car_Pool/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Pool car_Pool = db.car_Pools.Find(id);
            if (car_Pool == null)
            {
                return HttpNotFound();
            }
            return View(car_Pool);
        }

        // POST: Car_Pool/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Departure_Time,Price,Date_Created,Arrival_Time,Origin,Days_Available,Destination,Available_Seats,Owner,Notes")] Car_Pool car_Pool)
        {
            if (ModelState.IsValid)
            {
                car_Pool.Date_Created = (from c in db.car_Pools
                                         where c.Id == car_Pool.Id
                                         select c.Date_Created).SingleOrDefault();

                db.Entry(car_Pool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car_Pool);
        }

        // GET: Car_Pool/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car_Pool car_Pool = db.car_Pools.Find(id);
            if (car_Pool == null)
            {
                return HttpNotFound();
            }
            return View(car_Pool);
        }

        // POST: Car_Pool/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car_Pool car_Pool = db.car_Pools.Find(id);
            db.car_Pools.Remove(car_Pool);
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
