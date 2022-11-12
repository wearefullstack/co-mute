using comute.Models;

namespace comute.Services.CarPoolService;

public interface ICarPoolService
{
    Task<List<CarPoolInfo>> GetCarPools();
    Task<List<CarPool>> GetCarPoolCurrentUser(int userId);
    Task SaveCarPool(CarPool carPool);
}
