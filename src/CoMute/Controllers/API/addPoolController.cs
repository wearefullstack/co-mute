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
    public class addPoolController : ApiController
    {

        CarPoolEntities db = new CarPoolEntities();


        public HttpResponseMessage AddPool(CarPool pool)
        {
            var car_pool = new User_Car_Pool
            {
                Register_ID=pool.Register_ID,
                Origin=pool.Origin,
                Available_Seats= (int)pool.Available_Seats,
                Date_Created=DateTime.Now.Date,
                Days=pool.Days,
                Departure= (TimeSpan)pool.Departure,
                Expected_Arrival= (TimeSpan)pool.Expected_Arrival,
                Destination=pool.Destination,
                Notes=pool.Notes,   
                Number_Of_Passengers=0,
                User_Car_Pool_ID=pool.User_Car_Pool_ID,
                



            };
            db.User_Car_Pool.Add(car_pool);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, car_pool);
        }
    }
}
