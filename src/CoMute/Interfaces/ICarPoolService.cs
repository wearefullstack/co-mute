using CoMute.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using CoMute.Enums;

namespace CoMute.Interfaces
{
    public interface ICarPoolService
    {
        Task<int> AddCarPoolAsync(CarPool carPool);
        List<CarPool> GetCarPools(int userId, CarPoolFilters carPoolFilter);
        CarPool GetCarPool(int carPoolId);
        Task<int> UpdateCarPoolAsync(CarPool carPool);
    }
}
