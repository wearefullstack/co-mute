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
    public class Car_PoolController : ApiController
    {
        private FullStackDBEntities db = new FullStackDBEntities();

      
        [System.Web.Http.Route("api/Car_Pool/allCarPools")]
        [HttpGet]
        public System.Object getAllCarPool()
        {
            var carPools = (from a in db.Car_Pool
                             join b in db.Users on a.User_ID equals b.User_ID
                            where a.Available_Seats > 0
                             select new
                             {
                                 a.Car_Pool_ID,
                                 a.User_ID,
                                 a.Origin,
                                 a.Destination,
                                 a.Departure_Time,
                                 a.Expected_Arrival_Time,
                                 a.Days_Available,
                                 a.Available_Seats,
                                 a.Notes,
                                 Name = b.Name + " " + b.Surname

                             }).ToList();
            return carPools;
        }

        // GET: api/Car_Pool/5
        [ResponseType(typeof(Car_Pool))]
        public IHttpActionResult GetCar_Pool(int id)
        {
            Car_Pool car_Pool = db.Car_Pool.Find(id);
            if (car_Pool == null)
            {
                return NotFound();
            }

            return Ok(car_Pool);
        }


        // POST: api/Car_Pool
        [ResponseType(typeof(Car_Pool))]
        public IHttpActionResult PostCar_Pool(Car_Pool car_Pool)
        {
            //var user = db.Car_Pool.Where(usr => usr.User_ID == car_Pool.User_ID).FirstOrDefault();
            var pools = from a in db.Car_Pool
                        where a.User_ID == car_Pool.User_ID
                        select new
                        {
                            a.Departure_Time,
                            a.Expected_Arrival_Time
                        };
            foreach(var item in pools)
            {
                if((car_Pool.Departure_Time >= item.Departure_Time && car_Pool.Departure_Time <= item.Expected_Arrival_Time) ||
                   (car_Pool.Expected_Arrival_Time >= item.Departure_Time && car_Pool.Departure_Time <= item.Expected_Arrival_Time))
                {
                    return BadRequest();
                }
            }
            if(car_Pool.Departure_Time < car_Pool.Expected_Arrival_Time)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if (car_Pool.Departure_Time >= user.Departure_Time && car_Pool.Departure_Time <= user.Expected_Arrival_Time)
            //{
            //    return BadRequest();
            //}

            db.Car_Pool.Add(car_Pool);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = car_Pool.Car_Pool_ID }, car_Pool);
        }


     
    }
}