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
    //------------------------------------------- CreateCarpoolController : Amber Bruil ---------------------------------------------------------//
    public class CreateCarpoolController : ApiController
    {
        dbCoMuteEntities db = new dbCoMuteEntities();

        /// <summary>
        /// Adding carpool to table
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public HttpResponseMessage CreateCarpool(Carpool p)
        {
            
            var carpool = new tblUserCarPool
            {
                Origin = p.Origin,
                Avail_Seats = (int)p.AvailSeats,
                Days_Avail = p.DaysAvail,
                Depart_Time = (TimeSpan)p.DepartTime,
                Arrival_Time = (TimeSpan)p.ArrivalTime,
                Destination = p.Destination,
                Owner_Leader = p.OwnerLeader,
                UserID = p.UserID,
                Date_Created = DateTime.Now.Date,
                Date_Joined = DateTime.Now.Date,
                Notes = p.Notes, 
                PassengerPoolID = p.PassengerPoolID,
            };


            db.tblUserCarPools.Add(carpool);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, p);
        }
    }
    //--------------------------------------------------- 0o00ooo End of File ooo00o0 --------------------------------------------------------//
}