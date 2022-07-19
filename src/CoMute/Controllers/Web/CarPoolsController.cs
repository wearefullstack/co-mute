using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.Data;
using CoMute.Web.Models;

namespace CoMute.Web.Controllers
{
    public class CarPoolsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CarPools
        public async Task<ActionResult> Index()
        {
            return View(await db.CarPools.ToListAsync());
        }
        // GET: CarPools search
        public async Task<ActionResult> ShowSearch()
        {
            return View();
        }
        // POST: CarPools search results
        public async Task<ActionResult> ShowSearchResults( String SearchItem)
        {
            return View("Index", await db.CarPools.Where(j => j.Origin.Contains(SearchItem)).ToListAsync());
        }
        // GET: CarPools/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarPool carPool = await db.CarPools.FindAsync(id);
            if (carPool == null)
            {
                return HttpNotFound();
            }
            return View(carPool);
        }

        // GET: CarPools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarPools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,DepartureTime,ArrivalTime,Origin,Destination,DaysAvailable,SeatsAvailable,Owner,Notes")] CarPool carPool)
        {
            if (ModelState.IsValid)
            {
                db.CarPools.Add(carPool);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(carPool);
        }

        // GET: CarPools/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarPool carPool = await db.CarPools.FindAsync(id);
            if (carPool == null)
            {
                return HttpNotFound();
            }
            return View(carPool);
        }

        // POST: CarPools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,DepartureTime,ArrivalTime,Origin,Destination,DaysAvailable,SeatsAvailable,Owner,Notes")] CarPool carPool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carPool).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(carPool);
        }

        // GET: CarPools/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarPool carPool = await db.CarPools.FindAsync(id);
            if (carPool == null)
            {
                return HttpNotFound();
            }
            return View(carPool);
        }

        // POST: CarPools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CarPool carPool = await db.CarPools.FindAsync(id);
            db.CarPools.Remove(carPool);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //GET: JOIN/LEAVE CAR POOL


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
