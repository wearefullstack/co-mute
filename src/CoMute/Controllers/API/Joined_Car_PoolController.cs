using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CoMute.Web.Models;

namespace CoMute.Web.Controllers.API
{
    public class Joined_Car_PoolController : ApiController
    {
        private FullStackDBEntities db = new FullStackDBEntities();

        // GET: api/Joined_Car_Pool/5
        [ResponseType(typeof(Joined_Car_Pool))]
        public IHttpActionResult GetJoined_Car_Pool(int id)
        {
            Joined_Car_Pool joined_Car_Pool = db.Joined_Car_Pool.Find(id);
            if (joined_Car_Pool == null)
            {
                return NotFound();
            }

            return Ok(joined_Car_Pool);
        }


        [System.Web.Http.Route("api/Joined_Car_Pool/myCarPools/{id}")]
        [HttpGet]

        public System.Object getMyCarPool(int id)
        {
            var joinList = (from a in db.Joined_Car_Pool
                            join b in db.Car_Pool on a.Car_Pool_ID equals b.Car_Pool_ID
                             where a.User_ID == id
                             select new
                             {
                                 a.Joined_Car_Pool_ID,
                                 a.User_ID,
                                 a.Car_Pool_ID,
                                 a.Date_Joined,
                                 b.Origin,
                                 b.Destination,
                                 b.Departure_Time,
                                 b.Expected_Arrival_Time,
                                 b.Days_Available,
                                 b.Available_Seats,
                                 b.Notes

                             }).ToList();
            return joinList;
        }


        // POST: api/Joined_Car_Pool
        [ResponseType(typeof(Joined_Car_Pool))]
        public IHttpActionResult PostJoined_Car_Pool(Joined_Car_Pool joined_Car_Pool)
        {           
            var pools = from a in db.Joined_Car_Pool
                        where a.User_ID == joined_Car_Pool.User_ID
                        select new
                        {
                            a.Departure_Time,
                            a.Expected_Arrival_Time
                        };
            foreach (var item in pools)
            {
                if ((joined_Car_Pool.Departure_Time >= item.Departure_Time && joined_Car_Pool.Departure_Time <= item.Expected_Arrival_Time) ||
                   (joined_Car_Pool.Expected_Arrival_Time >= item.Departure_Time && joined_Car_Pool.Departure_Time <= item.Expected_Arrival_Time))
                {
                    return BadRequest();
                }
                
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var carPools = db.Car_Pool.Where(zz => zz.Car_Pool_ID == joined_Car_Pool.Car_Pool_ID).FirstOrDefault();
            carPools.Available_Seats--;
            db.Joined_Car_Pool.Add(joined_Car_Pool);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = joined_Car_Pool.Joined_Car_Pool_ID }, joined_Car_Pool);
        }

        // DELETE: api/Joined_Car_Pool/5
        [ResponseType(typeof(Joined_Car_Pool))]
        public IHttpActionResult DeleteJoined_Car_Pool(int id)
        {
            Joined_Car_Pool joined_Car_Pool = db.Joined_Car_Pool.Find(id);
            if (joined_Car_Pool == null)
            {
                return NotFound();
            }
            var carPools = db.Car_Pool.Where(zz => zz.Car_Pool_ID == joined_Car_Pool.Car_Pool_ID).FirstOrDefault();
            carPools.Available_Seats++;
            db.Joined_Car_Pool.Remove(joined_Car_Pool);
            db.SaveChanges();

            return Ok(joined_Car_Pool);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Joined_Car_PoolExists(int id)
        {
            return db.Joined_Car_Pool.Count(e => e.Joined_Car_Pool_ID == id) > 0;
        }
    }
}