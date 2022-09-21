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

        public CarPoolService()
        {
            _carPoolRepository = new CarPoolRepository();
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

        public List<CarPoolRequest> GetCarPool(string userName)
        {
            var returnedList  = new List<CarPoolRequest>();
            foreach (var item in _carPoolRepository.GetCarPoolsByUserName(userName)) returnedList.Add(GetCarPoolById(item.Id));
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