using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoMute.Web.Models;
using CoMute.Web.Models.DAL;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {

        public CarPoolController()
        {

        }
        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult GetAllCarPools()
        {
            DAL.displayCarPools();
            if (Car_Pool.carPools.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(Car_Pool.carPools);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public string GetCarPool(int carPoolID)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult PostNewCarPool(registerCarPoolRequest registerCarPoolRequest)
        {
            if (ModelState.IsValid)
            {
                registerCarPoolRequest.owner = Web.UserController.currentUser.UserID;
                DAL.registerCarPool(registerCarPoolRequest);

                return Ok();
            }
            else
            {
                return BadRequest("Invalid data.");
            }
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }


    }
}