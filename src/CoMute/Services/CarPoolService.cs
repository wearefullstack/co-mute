using CoMute.Web.Data;
using CoMute.Web.Data.Entities;
using CoMute.Web.Data.Repository;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CoMute.Web.Services
{
    public class CarPoolService
    {
        private readonly CarPoolRepository _carPoolRepository;
        private readonly UserService _userService;

        public CarPoolService()
        {
            _carPoolRepository = new CarPoolRepository();
            _userService = new UserService();    
        }

        public async Task addCarPool(CreateCarPoolRequest request)
        {
            await _carPoolRepository.AddCarPool(new CarPool
            {
                Avail_Seats = request.AvailableSeats,
                DaysAvailable = request.DaysAvailable,
                DepartureTime = request.DepartureTime,
                Destination = request.Destination,
                ExpectA_Time = request.ExpectArivalTime,
                Notes = request.Notes,
                Origin = request.Origin,
                Owner_Leader =request.Owner_Leader,
                PoolCreationDate = DateTime.Now,
            });   
        }
        
        public async Task JoinCarPool(int carPoolId, string userName)
        {
            var userProfile = _userService.GetUserProfileByEmail(userName);
            await _carPoolRepository.JoinCarPool(carPoolId, userProfile.UserId);
        }

        public List<CarPoolRequest> GetCarPool(string userName)
        {
            return MapCarPoolList(_carPoolRepository.GetCarPoolsByUserName(userName));
        }
        
        public List<CarPoolRequest> GetAllCarPools()
        {
            return MapCarPoolList(_carPoolRepository.GetAllCarPools());
        }
        
        public List<CarPoolRequest> SearchCarPools(string keyword)
        {
            return MapCarPoolList(_carPoolRepository.GetAllCarPools()).Where(x=>x.Origin.Contains(keyword)
             || x.Owner_Leader.Contains(keyword)|| x.Destination.Contains(keyword)).ToList();
        }
        
        private List<CarPoolRequest> MapCarPoolList(List<CarPool> carPools)
        {
            var returnedList  = new List<CarPoolRequest>();
            foreach (var item in carPools) returnedList.Add(GetCarPoolById(item.Id));
            return returnedList;
        }

        private CarPoolRequest GetCarPoolById(int Id)
        {
            var CarPoolRequest = _carPoolRepository.GetCarPoolById(Id);
            return new CarPoolRequest
            {
                Owner_Leader = CarPoolRequest.Owner_Leader,
                AvailableSeats = CarPoolRequest.Avail_Seats,
                DaysAvailable = CarPoolRequest.DaysAvailable,
                DepartureTime = CarPoolRequest.DepartureTime,
                PoolCreationDate = CarPoolRequest.PoolCreationDate,
                Destination = CarPoolRequest.Destination,
                ExpectArivalTime = CarPoolRequest.ExpectA_Time,
                Id = CarPoolRequest.Id,
                Notes = CarPoolRequest.Notes,
                Origin = CarPoolRequest.Origin
            };
        }
    }
}