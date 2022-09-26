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
    public class UpdateUserController : ApiController
    {
        public IHttpActionResult Put(RegistrationRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            using (var ctx = new CarPoolEntities())
            {
                var existingUser = ctx.Registers.Where(s => s.Register_ID == user.RegisterID).FirstOrDefault();

                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.Surname = user.Surname;
                    existingUser.Email = user.EmailAddress;
                    existingUser.Phone =Convert.ToInt32( user.PhoneNumber);

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
