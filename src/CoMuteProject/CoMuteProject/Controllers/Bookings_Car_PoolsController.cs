using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoMuteProject.Models;
using Microsoft.AspNet.Identity;

namespace CoMuteProject.Controllers
{
    [Authorize]
    public class Bookings_Car_PoolsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings_Car_Pools
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Car_Pool);
            return View(bookings.ToList());
        }

        public ActionResult My_Bookings()
        {
            var bookings = db.Bookings.Include(b => b.Car_Pool);
            var user = User.Identity.GetUserId();
            bookings = from c in bookings
                       where c.UserId == user
                       select c;

            return View(bookings.ToList());
        }


        // GET: Bookings_Car_Pools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings_Car_Pools bookings_Car_Pools = db.Bookings.Find(id);
            if (bookings_Car_Pools == null)
            {
                return HttpNotFound();
            }
            return View(bookings_Car_Pools);
        }

        // GET: Bookings_Car_Pools/Create
        public ActionResult Create(int ?id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var car_Pool = (from c in db.car_Pools
                            where c.Id == id
                            select c).SingleOrDefault();

            if (car_Pool == null && car_Pool.Available_Seats <= 0)
            {
                return HttpNotFound();
            }
          
            ViewBag.Car_Pool = car_Pool;
            return View();
        }

        // POST: Bookings_Car_Pools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Car_PoolId,Time,UserId")] Bookings_Car_Pools bookings_Car_Pools, int? id)
        {
            var car_Pool = (from c in db.car_Pools
                            where c.Id == id
                            select c).SingleOrDefault();

            if (ModelState.IsValid)
            {
                var Seats = car_Pool.Available_Seats - 1;
                car_Pool.Available_Seats = Seats;

                bookings_Car_Pools.Car_PoolId = (int)id;
                bookings_Car_Pools.Time = DateTime.Now;
                bookings_Car_Pools.UserId = User.Identity.GetUserId();
                db.Bookings.Add(bookings_Car_Pools);
                db.Entry(car_Pool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("My_Bookings");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            if (car_Pool == null)
            {
                return HttpNotFound();
            }

            ViewBag.Car_Pool = car_Pool;

            return View(bookings_Car_Pools);
        }

        // GET: Bookings_Car_Pools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings_Car_Pools bookings_Car_Pools = db.Bookings.Find(id);
            if (bookings_Car_Pools == null)
            {
                return HttpNotFound();
            }
            if(bookings_Car_Pools.UserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Car_PoolId = new SelectList(db.car_Pools, "Id", "Origin", bookings_Car_Pools.Car_PoolId);
            return View(bookings_Car_Pools);
        }

        // POST: Bookings_Car_Pools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Car_PoolId,Date,Time,UserId")] Bookings_Car_Pools bookings_Car_Pools)
        {
            if (ModelState.IsValid)
            {
                bookings_Car_Pools.UserId = User.Identity.GetUserId();
                db.Entry(bookings_Car_Pools).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("My_Bookings");
            }
            ViewBag.Car_PoolId = new SelectList(db.car_Pools, "Id", "Origin", bookings_Car_Pools.Car_PoolId);
            return View(bookings_Car_Pools);
        }

        // GET: Bookings_Car_Pools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookings = db.Bookings.Include(b => b.Car_Pool);

           var booking = (from c in bookings
                        where c.Id == id
                        select c).SingleOrDefault();

            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings_Car_Pools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bookings_Car_Pools bookings_Car_Pools = db.Bookings.Find(id);
            var Id = bookings_Car_Pools.Car_PoolId;

            var car_Pool = (from c in db.car_Pools
                            where c.Id == Id
                            select c).SingleOrDefault();

            var Seats = car_Pool.Available_Seats - 1;
            car_Pool.Available_Seats = Seats;

            db.Bookings.Remove(bookings_Car_Pools);
            db.Entry(car_Pool).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("My_Bookings");
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
