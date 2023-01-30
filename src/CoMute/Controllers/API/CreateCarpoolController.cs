using CoMute.Web.Data;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{

    public class CreateCarpoolController : ApiController
    {
        dbCoMuteEntities db = new dbCoMuteEntities();

        public HttpResponseMessage CreateCarpool(Carpool pool)
        {
            var carpool = new tblUserCarPool
            {
                Origin = pool.Origin,
                Avail_Seats = (int)pool.AvailSeats,
                Days_Avail = pool.DaysAvail,
                Depart_Time = (TimeSpan)pool.DepartTime,
                Arrival_Time = (TimeSpan)pool.ArrivalTime,
                Destination = pool.Destination,
                Date_Created = DateTime.Now.Date,
                Notes = pool.Notes,
            };

            db.tblUserCarPools.Add(carpool);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, carpool);
        }
    }
}