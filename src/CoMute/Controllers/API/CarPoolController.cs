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
    public class CarPoolController : ApiController
    {
        CarPoolEntities db = new CarPoolEntities();



        // GET: Car Pools
       /* [Route("GetCarPools")]
        [HttpGet]*/
        public List<CarPool> GetCarPool()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var carpool = db.User_Car_Pool.Select(zz => new CarPool
            {
                User_Car_Pool_ID = zz.User_Car_Pool_ID,
                Available_Seats = zz.Available_Seats,
                Date_Created = zz.Date_Created,
                Days = zz.Days,
                Departure = zz.Departure,
                Expected_Arrival = zz.Expected_Arrival,
                Notes = zz.Notes,
                Origin = zz.Origin,
                Destination = zz.Destination,
                Number_Of_Passengers = zz.Number_Of_Passengers,
                Available_Spots= (int)((zz.Available_Seats)-(zz.Number_Of_Passengers)),
                Name=zz.Register.Name,
                Surname=zz.Register.Surname,



            }).Where(zz=>zz.Number_Of_Passengers<zz.Available_Seats).ToList();
            return carpool;

        }
    }
        
}
