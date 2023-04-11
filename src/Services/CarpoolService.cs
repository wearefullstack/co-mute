using FSWebApi.Data;
using FSWebApi.Dto.Carpool;
using FSWebApi.Interfaces;
using FSWebApi.Models;
using FSWebApi.Utils.ErrorHandling;
using Microsoft.EntityFrameworkCore;

namespace FSWebApi.Services
{
    public class CarpoolService : ICarpoolService
    {
        AppDbContext _context;
        public CarpoolService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CarpoolDTO> GetAllCarpools()
        {
            var carpools = _context.carpools.ToList();
            List<CarpoolDTO> res = new List<CarpoolDTO>();

            carpools.ForEach(x =>
            {
                res.Add(MapCarpoolToCarpoolDTO(x));
            });

            return res;
        }

        public IEnumerable<CarpoolDTO> GetAvailableCarpools(Guid userId)
        {
            var carpools = _context.carpools.Where(x => x.OwnerID != userId).ToList();
            List<CarpoolDTO> res = new List<CarpoolDTO>();

            carpools.ForEach(x =>
            {
                res.Add(MapCarpoolToCarpoolDTO(x));
            });

            return res;
        }

        public IEnumerable<CarpoolDTO> GetCreatedCarpools(Guid userId)
        {
            var carpools = _context.carpools.Where(x => x.OwnerID == userId).ToList();

            List<CarpoolDTO> res = new List<CarpoolDTO>();

            carpools.ForEach(x =>
            {
                res.Add(MapCarpoolToCarpoolDTO(x));
            });

            return res;
        }

        public IEnumerable<CarpoolDTO> GetJoinedCarpools(Guid userId)
        {
            var carpools = _context.carpools.Where(x => x.Members.Any(z => z.UserId == userId)).ToList();
            List<CarpoolDTO> res = new List<CarpoolDTO>();

            carpools.ForEach(x =>
            {
                res.Add(MapCarpoolToCarpoolDTO(x));
            });

            return res;
        }

        public CarpoolDTO RegisterCarpool(CreateCarpoolDTO createCarpoolDTO)
        {

            //Validate if carpool times overlap with other carpools
            if(DoCarpoolsOverlap(createCarpoolDTO.OwnerID, createCarpoolDTO.DayAvailable, createCarpoolDTO.DepartureTime, createCarpoolDTO.ArrivalTime))
                throw new AppException("Could not create carpool: The time-frames of this carpool overlap with another carpool you have joined or created");

            //Validate dates
            if(createCarpoolDTO.DepartureTime >= createCarpoolDTO.ArrivalTime)
                throw new AppException("Could not create carpool: The departure time cannot be after the arrival time");


            Carpool newCarpool = new Carpool()
            {
                CarpoolId = Guid.NewGuid(),
                Origin = createCarpoolDTO.Origin,
                Destination = createCarpoolDTO.Destination,
                DayAvailable = createCarpoolDTO.DayAvailable.ToDateTime(new TimeOnly()),
                DepartureTime = createCarpoolDTO.DepartureTime.ToTimeSpan(),
                ArrivalTime = createCarpoolDTO.ArrivalTime.ToTimeSpan(),
                Notes = createCarpoolDTO.Notes,
                Seats = createCarpoolDTO.Seats,
                OwnerID = createCarpoolDTO.OwnerID,
                DateCreated = DateTime.Now
            };

            _context.carpools.Add(newCarpool);
            _context.SaveChanges();

            return MapCarpoolToCarpoolDTO(newCarpool);
        }


        public CarpoolDTO JoinCarpool(JoinCarpoolDTO joinCarpoolDTO)
        {
            var carpool = _context.carpools.Where(x => x.CarpoolId == joinCarpoolDTO.CarpoolId).Include(x => x.Members).FirstOrDefault();
            if (carpool == null)
                throw new AppException("Carpool not found");

            //check if user is not already a member of this carpool
            if (carpool.Members.Any(x=> x.CarpoolId == joinCarpoolDTO.CarpoolId && x.UserId == joinCarpoolDTO.UserId))
                throw new AppException("You are already a member of this carpool");


            //check if carpool times overlap
            if (DoCarpoolsOverlap(joinCarpoolDTO.UserId, DateOnly.FromDateTime(carpool.DayAvailable), TimeOnly.FromTimeSpan(carpool.DepartureTime), TimeOnly.FromTimeSpan(carpool.ArrivalTime)))
                throw new AppException("Could not join carpool: The time-frames of this carpool overlap with another carpool you have joined or created");


            //Check if there are available seats in the carpool
            int NumCarpoolMembers = _context.carpoolMembers.Where(x => x.CarpoolId == carpool.CarpoolId).Count();
            if(carpool.Seats - NumCarpoolMembers <= 0)
                throw new AppException("Could not join carpool: There are no available seats in this carpool");


            CarpoolMember carpoolMember = new CarpoolMember()
            {
                CarpoolId = joinCarpoolDTO.CarpoolId,
                UserId = joinCarpoolDTO.UserId,
                JoinDate = DateTime.Now,
            };
       
            _context.carpoolMembers.Add(carpoolMember);
            _context.SaveChanges();
       
            CarpoolDTO carpoolDTO = MapCarpoolToCarpoolDTO(carpool);
            return carpoolDTO;
        }

        
        public bool ExitCarpool(Guid carpoolId, Guid userId)
        {
            var carpoolMember = _context.carpoolMembers.Where(x => x.UserId == userId && x.CarpoolId == carpoolId).FirstOrDefault();
            if (carpoolMember == null)
                throw new AppException("You are not a member of this carpool");

            _context.carpoolMembers.Remove(carpoolMember);
            _context.SaveChanges();
            return true;
        }

            
        private bool DoCarpoolsOverlap(Guid userId, DateOnly dayAvailable, TimeOnly departureTime, TimeOnly arrivalTime)
        {
            bool res = false;

            var carpools = _context.carpools.Where(x => x.OwnerID == userId || x.Members.Any(z => z.UserId == userId)).ToList();

            foreach (var carpool in carpools)
            {
                if (DateOnly.FromDateTime(carpool.DayAvailable) == dayAvailable)
                {
                    TimeOnly secondDepartureTime = TimeOnly.FromTimeSpan(carpool.DepartureTime);
                    TimeOnly secondArrivalTime = TimeOnly.FromTimeSpan(carpool.ArrivalTime);

                    res = (!secondDepartureTime.IsBetween(departureTime, arrivalTime)
                        || !secondArrivalTime.IsBetween(departureTime, arrivalTime)
                            || !departureTime.IsBetween(secondDepartureTime, secondArrivalTime)
                                || !arrivalTime.IsBetween(secondDepartureTime, secondArrivalTime));
                }

                if (res)
                {
                    break;
                }

            }

            return res;
        }

        private CarpoolDTO MapCarpoolToCarpoolDTO(Carpool carpool)
        {
            //getting taken seats
            int NumCarpoolMembers = _context.carpoolMembers.Where(x => x.CarpoolId == carpool.CarpoolId).Count();

            //Getting ownwers details
            var owner = _context.users.Where(x => x.UserId == carpool.OwnerID).FirstOrDefault();
            string ownerDetail;
            ownerDetail = (owner != null) ? $"{owner.Name} {owner.Surname}" : "Unkown Owner";

            CarpoolDTO newCarpoolDTO = new CarpoolDTO()
            {
                CarpoolId = carpool.CarpoolId,
                Origin = carpool.Origin,
                Destination = carpool.Destination,
                DayAvailable = DateOnly.FromDateTime(carpool.DayAvailable),
                DepartureTime = TimeOnly.FromTimeSpan(carpool.DepartureTime),
                ArrivalTime = TimeOnly.FromTimeSpan(carpool.ArrivalTime),
                Notes = carpool.Notes,
                Seats = carpool.Seats,
                AvailableSeats = carpool.Seats - NumCarpoolMembers, //Calculate Available Seats
                               
                OwnerID = carpool.OwnerID,
                OwnerDetails = ownerDetail,
                DateCreated = carpool.DateCreated,
            };

            return newCarpoolDTO;
        }
    }
}
