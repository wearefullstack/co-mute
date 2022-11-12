using comute.Models;
using ErrorOr;

namespace comute.Services.CarPoolService;

public interface ICarPoolService
{
    Task<List<CarPoolInfo>> GetCarPools();
    Task<List<CarPoolInfo>> GetCarPoolCurrentUser(int userId);
    Task SaveCarPool(CarPool carPool);
}
