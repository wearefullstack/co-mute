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
    public class BookPoolController : ApiController
    {

        CarPoolEntities db = new CarPoolEntities();


        public HttpResponseMessage BookPool(PassengerCarPool join)
        {


            var user = new Passenger_Pool
            {
                Register_ID = join.Register_ID,
                User_Car_Pool_ID = join.User_Car_Pool_ID,
                Date_Joined = DateTime.Now.Date,
                Passenger_Pool_ID = join.Passenger_Pool_ID,
                Status_ID = 1
            
              

            };
            
            db.Passenger_Pool.Add(user);
           
            db.SaveChanges();

           

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }
    }
}
