using CoMute.Web.Models.Dto;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;


namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
        [Route("CarPool/add")]
        public int Post(CarPoolRequest model)
        {
            var context = new CoMuteEntities();
            try
            {

                var user = new CarPool()
                {
                    DepartureTime = model.DepartureTime,
                    ExpectedArrivalTime = model.ExpectedArrivalTime,
                    Origin = model.Origin,
                    DaysAvailable = model.DaysAvailable,
                    Destination = model.Destination,
                    AvilableSeats = model.AvilableSeats,
                    OwnerId = model.OwnerId,
                    Notes = model.Notes,
                };

                context.CarPools.Add(user);
                context.SaveChangesAsync();

                return 1;
            }
            catch
            {
                return -1;
            }

        }

        [Route("CarPool/GetCarPools")]
        public List<CarPoolRequest> GetCarPools()
        {
            try
            {
                var context = new CoMuteEntities();

                var results = (from x in context.CarPools
                               join y in context.Users on x.OwnerId equals y.Id
                               select new CarPoolRequest
                               {
                                   Id = x.Id,
                                   DepartureTime = x.DepartureTime,
                                   ExpectedArrivalTime = x.ExpectedArrivalTime,
                                   Origin = x.Origin,
                                   DaysAvailable = x.DaysAvailable,
                                   Destination = x.Destination,
                                   AvilableSeats = x.AvilableSeats,
                                   OwnerId = x.OwnerId,
                                   OwnerName = y.Name,
                                   Notes = x.Notes

                               }).ToList();

                if (results is null)
                    return new List<CarPoolRequest>();

                return results;
            }
            catch
            {
                return new List<CarPoolRequest>();
            }
        }

        [Route("CarPool/GetCarPoolById")]
        public CarPoolRequest GetCarPoolById(int Id)
        {
            try
            {
                var context = new CoMuteEntities();

                var results = (from x in context.CarPools.Where(x => x.Id == Id)
                               join y in context.Users on x.OwnerId equals y.Id
                               select new CarPoolRequest
                               {
                                   Id = x.Id,
                                   DepartureTime = x.DepartureTime,
                                   ExpectedArrivalTime = x.ExpectedArrivalTime,
                                   Origin = x.Origin,
                                   DaysAvailable = x.DaysAvailable,
                                   Destination = x.Destination,
                                   AvilableSeats = x.AvilableSeats,
                                   OwnerId = x.OwnerId,
                                   OwnerName = y.Name,
                                   Notes = x.Notes

                               }).FirstOrDefault();

                if (results is null)
                    return new CarPoolRequest();

                return results;
            }
            catch
            {
                return new CarPoolRequest();
            }
        }


        // Joined pools
        [Route("CarPool/GetJoinedCarPools")]
        public List<CarPoolRequest> GetJoinedCarPools(int Id)
        {
            try
            {
                var context = new CoMuteEntities();

                var results = (from x in context.CarPools
                               join z in context.CarPoolUsers on x.Id equals z.CarPoolId
                               join y in context.Users.Where(t => t.Id == Id) on z.UserId equals y.Id
                               select new CarPoolRequest
                               {
                                   Id = x.Id,
                                   DepartureTime = x.DepartureTime,
                                   ExpectedArrivalTime = x.ExpectedArrivalTime,
                                   Origin = x.Origin,
                                   DaysAvailable = x.DaysAvailable,
                                   Destination = x.Destination,
                                   AvilableSeats = x.AvilableSeats,
                                   OwnerId = x.OwnerId,
                                   OwnerName = y.Name,
                                   Notes = x.Notes

                               }).ToList();

                if (results is null)
                    return new List<CarPoolRequest>();

                return results;
            }
            catch
            {
                return new List<CarPoolRequest>();
            }
        }

        // Joined pools
        [Route("CarPool/GetAvailableCarPools")]
        public List<CarPoolRequest> GetAvailableCarPools(int Id)
        {
            try
            {
                var context = new CoMuteEntities();

                var results = (from x in context.CarPools.Where(x=> x.CarPoolUsers.Count() < x.AvilableSeats)
                               //join z in context.CarPoolUsers on x.Id equals z.CarPoolId
                               //join y in context.Users.Where(t => t.Id == Id) on z.UserId equals y.Id
                               where  x.CarPoolUsers.Where(z=> z.UserId == Id).Count() <=0
                               select new CarPoolRequest
                               {
                                   Id = x.Id,
                                   DepartureTime = x.DepartureTime,
                                   ExpectedArrivalTime = x.ExpectedArrivalTime,
                                   Origin = x.Origin,
                                   DaysAvailable = x.DaysAvailable,
                                   Destination = x.Destination,
                                   AvilableSeats = x.AvilableSeats,
                                   OwnerId = x.OwnerId,
                                   OwnerName = x.User.Name,
                                   Notes = x.Notes

                               }).ToList();

                if (results is null)
                    return new List<CarPoolRequest>();

                return results;
            }
            catch
            {
                return new List<CarPoolRequest>();
            }
        }

        // Join pool
        [Route("CarPool/JoinPool")]
        public int JoinCarPool(UserPool model)
        {
            try
            {
                var context = new CoMuteEntities();

                var results = new CarPoolUser ()
                {
                    CarPoolId = model.poolId,
                    UserId = model.userId
                };

                context.CarPoolUsers.Add(results);
                context.SaveChanges();

                return 1;
            }
            catch
            {
                return -1;
            }
        }

        [Route("CarPool/LeavePool")]
        public int LeaveCarPool(UserPool model)
        {
            try
            {
                var context = new CoMuteEntities();

                var recored = context.CarPoolUsers.FirstOrDefault(x=> x.UserId == model.userId && x.CarPoolId == model.poolId);
                if(recored is null)
                    return -1;

                context.CarPoolUsers.Remove(recored);
                context.SaveChanges();

                return 1;
            }
            catch
            {
                return -1;
            }
        }


    }
 }