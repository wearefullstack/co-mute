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
    public class updatePassngersController : ApiController
    {
        public IHttpActionResult Put(CarPool user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            using (var ctx = new CarPoolEntities())
            {
                var existingUser = ctx.User_Car_Pool.Where(s => s.User_Car_Pool_ID == user.User_Car_Pool_ID).FirstOrDefault();

                if (existingUser != null)
                {
                    existingUser.Number_Of_Passengers = existingUser.Number_Of_Passengers -1;


                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
    }
}
