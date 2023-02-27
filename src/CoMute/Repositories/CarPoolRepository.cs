using CoMute.Web.Context;
using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Repositories
{
    public class CarPoolRepository
    {

        private readonly CoMuteDbContext _context;

        public CarPoolRepository()
        {
            _context = new CoMuteDbContext();

        }

        public bool AddCarpool(CarPool carpool)
        {

            carpool.CreatedDate = DateTime.Now;
            carpool.ModifiedDate = DateTime.Now;


            _context.CarPools.Add(carpool);
            _context.SaveChanges();

            return true;
        }

        public List<CarPool> GetCarpools()
        {
            var _availableCarPools = _context.CarPools;//.Where(p => p.AvailableSeats>0 && p.UserGuid != userGuid);
            return _availableCarPools.ToList();
        }
        public List<CarPool> GetAvailableCarpools(Guid userGuid)
        {
            List<CarPool> carPools = new List<CarPool>();

            var _availableCarPools = _context.CarPools.Where(p => p.AvailableSeats > 0 && p.UserGuid != userGuid).ToList();

            if(_availableCarPools != null)
            {
                foreach (var item in _availableCarPools)
                {
                    string formattedDate = item.CreatedDate.ToString("MM/dd/yyyy");
                    item.CreatedDate = DateTime.Parse(formattedDate);
                    carPools.Add(item);
                }

                return carPools;
            }

            return _availableCarPools.ToList();
        }

        public List<CarPool> SearchCarPool(string origin, string destination)
        {
            var searchedList = _context.CarPools.Where(p => p.Origin.Contains(origin) || p.Destination.Contains(destination));


            return searchedList.ToList();
        }

        public bool JoinPool(Guid carpoolGuid, Guid userGuid)
        {
            var existingCarpool = _context.CarPools.FirstOrDefault(c => c.CarPoolGuid == carpoolGuid);

            if (existingCarpool == null)
            {
                return false; // Carpool not found
            }

            //Verify if seats still available
            if (existingCarpool.AvailableSeats <= 0)
            {
                return false; // No available seats
            }
            var _member = new CarPoolMembers()
            {
                CarPoolMemberGuid = Guid.NewGuid(),
                CarPoolGuid = carpoolGuid,
                UserGuid = userGuid,
                JoinedDate = DateTime.Now,
                StatusDate = DateTime.Now,
                Status = "Active"
            };

            _context.CarPoolMembers.Add(_member);
            _context.SaveChanges();

            //Update Available seats
            UpdateAvailableSeats(existingCarpool, -1);
            
            return true;
        }

        public bool LeaveCarPool(Guid carpoolGuid, Guid userGuid) 
        {
            var existingCarpool = _context.CarPools.FirstOrDefault(c => c.CarPoolGuid == carpoolGuid);

            if (existingCarpool == null)
            {
                return false;
            }

            var carPoolMember = _context.CarPoolMembers.FirstOrDefault(c => c.UserGuid == userGuid);

            if (carPoolMember == null)
            {
                return false;
            }

            carPoolMember.Status = "InActive";

            _context.Entry(carPoolMember).CurrentValues.SetValues(carPoolMember);
            _context.SaveChanges();

            UpdateAvailableSeats(existingCarpool, 1);

            return true;
        }

        public List<CarPoolJoinedByMember> GetJoinedCarPool(Guid userGuid)
        {
            List<CarPool> carPools = new List<CarPool>();
            List<CarPoolMembers> carPoolsMember = new List<CarPoolMembers>();
            List<CarPoolMembers> carPoolsMemberJoined = new List<CarPoolMembers>();
            List<CarPoolJoinedByMember> carPoolsJoined = new List<CarPoolJoinedByMember>();

            carPools = _context.CarPools.ToList();

            carPoolsMember = _context.CarPoolMembers.Where(c => c.Status == "Active").ToList();

            foreach (var item in carPoolsMember)
            {
                if (item.UserGuid == userGuid)
                {
                    carPoolsMemberJoined.Add(item);
                }
 
            }

            var carPoolsResults = from c in carPools
                                  join mc in carPoolsMemberJoined
                                  on c.CarPoolGuid equals mc.CarPoolGuid
                                  select new
                                  {
                                      CarPoolGuid = c.CarPoolGuid,
                                      Origin = c.Origin,
                                      Destination = c.Destination,
                                      DepartureTime = c.DepartureTime,
                                      ExpectedArrivalTime = c.ExpectedArrivalTime,
                                      AvailableSeats = c.AvailableSeats,
                                      Notes = c.Notes,
                                      JoindDate = mc.JoinedDate,
                                      userGuid = mc.UserGuid,
                                      Status = mc.Status
                                  };

            if (carPoolsResults != null)
            {
                foreach (var item in carPoolsResults)
                {
                    CarPoolJoinedByMember carPooljoined = new CarPoolJoinedByMember() { 
                        CarPoolGuid = item.CarPoolGuid,
                        Origin = item.Origin,
                        Destination = item.Destination,
                        DepartureTime = item.DepartureTime,
                        ExpectedArrivalTime = item.ExpectedArrivalTime,
                        AvailableSeats = item.AvailableSeats,
                        Notes = item.Notes,
                        JoindDate = item.JoindDate,
                        Status = item.Status,
                        UserGuid = item.userGuid

                    };

                    carPoolsJoined.Add(carPooljoined);
                }
            }
            

            return carPoolsJoined;
        }

        private void UpdateAvailableSeats(CarPool existingCarpool,int _change)
        {
            var _poolUpdate = existingCarpool;
            _poolUpdate.AvailableSeats = _poolUpdate.AvailableSeats + (_change);
            _poolUpdate.ModifiedDate = DateTime.Now;
            _context.Entry(existingCarpool).CurrentValues.SetValues(_poolUpdate);
            _context.SaveChanges();
        }

        private bool CarpoolExists(CarPool carpool)
        {
            return _context.CarPools.Any(c => c.Equals(carpool));
        }

    }
}