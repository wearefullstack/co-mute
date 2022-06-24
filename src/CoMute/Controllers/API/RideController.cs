using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoMute.Web.Models.Dto;
using DataLibrary;

namespace CoMute.Web.Controllers.API
{
    public class RideController : ApiController
    {
        [Route("search/create")] // Unsure of navigation, used Postman for assistance late in development
        public HttpResponseMessage Post(RideRequest rideCreate) // Create Ride Opportunities
        {
                var Ride = new DataLibrary.Ride()
                {
                    DepartureTime = rideCreate.DepartureTime,
                    ArrivalTime = rideCreate.ArrivalTime,
                    Origin = rideCreate.Origin,
                    Destination = rideCreate.Destination,
                    AvailableSeats = rideCreate.AvailableSeats,
                    UserId = rideCreate.UserId,
                    Created = DateTime.Now,
                    Notes = rideCreate.Notes
                };

            using (FullStackTestEntities entities = new FullStackTestEntities()) // Save New Ride to the RidesTable
            {
                var Rides = entities.Rides.Where(x => x.UserId == rideCreate.UserId && Ride.DepartureTime < x.ArrivalTime && Ride.ArrivalTime > x.DepartureTime).FirstOrDefault();
                if (Rides != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }
                entities.Rides.Add(Ride);
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("index/remove")]
        public void Delete(RideDeleteRequest rideDeleteRequest) // Delete Ride Opportunities from Rides Table and from Users' Joined Table
        {
            using (FullStackTestEntities entities = new FullStackTestEntities())
            {
                var Ride = entities.Rides.Where(x => x.RideId == rideDeleteRequest.RideId).FirstOrDefault(); // for Rides
                entities.Rides.Remove(Ride);

                entities.RideUsers.RemoveRange(entities.RideUsers.Where(x => x.RideId == rideDeleteRequest.RideId)); // for Users on Rides
                entities.SaveChanges();
            }
        }

        [Route("search")] 
        public IEnumerable<DataLibrary.Ride> Get() // View Ride Opportunities in Search
        {
            using (FullStackTestEntities entities = new FullStackTestEntities())
            {
                return entities.Rides.Where(x => x.DepartureTime > DateTime.Now && x.AvailableSeats > 0).ToList();
            }
        }

        [Route("search/join")] 
        public void Put(RideJoinRequest rideJoinRequest) // Join Ride Opportunities in Search/RideTable with BtnJoin in last columb interactive table
        {
            using (FullStackTestEntities entities = new FullStackTestEntities())
            {
                var rideUser = new RideUser
                {
                    RideId = rideJoinRequest.RideId,
                    UserId = rideJoinRequest.UserId
                };
                entities.RideUsers.Add(rideUser);

                var ride = entities.Rides.Where(x => x.RideId == rideJoinRequest.RideId).FirstOrDefault();
                ride.AvailableSeats--;
                entities.SaveChanges();
            }
        }

        [Route("index/joined")] 
        public void Put(RideLeaveRequest rideLeaveRequest) // Leave Ride Opportunities, spot becomes available once a user leaves the CarPool
        {
            using (FullStackTestEntities entities = new FullStackTestEntities())
            {
                var rideUsers = entities.RideUsers.Where(x => x.RideId == rideLeaveRequest.RideId).FirstOrDefault();
                entities.RideUsers.Remove(rideUsers);

                var ride = entities.Rides.Where(x => x.RideId == rideLeaveRequest.RideId).FirstOrDefault();
                ride.AvailableSeats++;
                entities.SaveChanges();
            }
        }
    }
}
