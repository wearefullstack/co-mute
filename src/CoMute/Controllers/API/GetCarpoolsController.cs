using CoMute.Web.Data;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.API
{
    public class GetCarpoolsController : ApiController
    {
        dbCoMuteEntities db = new dbCoMuteEntities();

        public object GetCarPool(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var carpool = db.tblUserCarPools.Select(c => new Carpool
            {
                CarpoolID = c.CarPoolID,
                AvailSeats = c.Avail_Seats,
                DateCreated = c.Date_Created,
                DaysAvail = (int)c.Days_Avail,
                DepartTime = c.Depart_Time,
                ArrivalTime = c.Arrival_Time,
                Notes = c.Notes,
                Origin = c.Origin,
                Destination = c.Destination,
                UserID = c.UserID,
                PassengerPoolID = c.PassengerPoolID,
            }).Where(c => c.UserID == id).ToList();



            return carpool;

        }
    }
}