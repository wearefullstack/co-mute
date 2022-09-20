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
                Avail_Seats = request.Avail_Seats,
                DaysAvailable = request.DaysAvailable,
                DepartureTime = request.DepartureTime,
                Destination = request.Destination,
                ExpectA_Time = request.ExpectA_Time,
                Notes = request.Notes,
                Origin = request.Origin,
                PoolCreationDate = DateTime.Now,
            });   
        }
    }
}