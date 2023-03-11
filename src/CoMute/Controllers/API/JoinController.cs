using CoMute.Web.Models;
using CoMute.Web.Models.DAL;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class JoinController : ApiController
    {
        public HttpResponseMessage Post(CarPoolID carPoolID)
        {
            ComuteDBEntities db = new ComuteDBEntities();
            try
            {
                CarpoolsTable carpool = db.CarpoolsTables.FirstOrDefault(x => x.CarpoolID == carPoolID.ID);
                CarpoolsJoined goJoin = new CarpoolsJoined();
                goJoin.UserJoinID = LoggedInUser.Id;
                goJoin.TheCarPoolID = carPoolID.ID;
                int availSeats = carpool.AvailableSeats;

                if(carPoolID.ID == 0 && carpool==null)
                {
                    throw new Exception();
                }

                if(availSeats <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                availSeats--;
                carpool.AvailableSeats = availSeats;


                db.CarpoolsJoineds.Add(goJoin);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}