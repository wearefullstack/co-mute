using CoMute.Web.Data;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class timesController : ApiController
    {
        CarPoolEntities db = new CarPoolEntities();
        public object GetCarPool(usertimes times)
        {

            db.Configuration.ProxyCreationEnabled = false;
            var carpool = db.Passenger_Pool.Select(zz => new CarPool
            {
                Passenger_Pool_ID = zz.Passenger_Pool_ID,
                User_Car_Pool_ID = zz.User_Car_Pool.User_Car_Pool_ID,
                Available_Seats = zz.User_Car_Pool.Available_Seats,
                Date_Created = zz.User_Car_Pool.Date_Created,
                Days = zz.User_Car_Pool.Days,
                Departure = zz.User_Car_Pool.Departure,
                Expected_Arrival = zz.User_Car_Pool.Expected_Arrival,
                Notes = zz.User_Car_Pool.Notes,
                Origin = zz.User_Car_Pool.Origin,
                Destination = zz.User_Car_Pool.Destination,
                Register_ID = zz.Register_ID,
                Number_Of_Passengers = zz.User_Car_Pool.Number_Of_Passengers,



            }).Where(zz => zz.Register_ID == times.Register_ID && times.Departure>= zz.Departure && times.Expected_Arrival<=zz.Expected_Arrival).ToList();
            return carpool;

        }
    }
}
