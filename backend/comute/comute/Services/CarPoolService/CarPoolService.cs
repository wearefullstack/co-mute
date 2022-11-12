using comute.Data;
using comute.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace comute.Services.CarPoolService;

public class CarPoolService : ICarPoolService
{
    private readonly DataContext _context;
    public CarPoolService(DataContext context) => _context = context;

    public Task<List<CarPoolInfo>> GetCarPoolCurrentUser(int userId)
    { 
        var results = (from carPool in _context.CarPools
                       join user in _context.Users
                       on carPool.Owner equals user.UserId
                       where user.UserId == userId
                       select new CarPoolInfo()
                       {
                           CarPoolId = carPool.CarPoolId,
                           Origin = carPool.Origin,
                           Destination = carPool.Destination,
                           DepartureTime = carPool.DepartureTime,
                           ExpectedArrivalTime = carPool.ExpectedArrivalTime,
                           DaysAvailable = carPool.DaysAvailable.Split().ToList(),
                           AvailableSeats = carPool.AvailableSeats,
                           Owner = carPool.Owner,
                           User = new(),
                           Notes = carPool.Notes,
                           Active = carPool.Active
                       }).ToListAsync();
        return results;
    }

    public Task<List<CarPoolInfo>> GetCarPools()
    {
        var results = (from carPool in _context.CarPools
                       join user in _context.Users
                       on carPool.Owner equals user.UserId
                       select new CarPoolInfo()
                       {
                           CarPoolId = carPool.CarPoolId,
                           Origin = carPool.Origin,
                           Destination = carPool.Destination,
                           DepartureTime = carPool.DepartureTime,
                           ExpectedArrivalTime = carPool.ExpectedArrivalTime,
                           DaysAvailable = carPool.DaysAvailable.Split().ToList(),
                           AvailableSeats = carPool.AvailableSeats,
                           Owner = carPool.Owner,
                           User = new()
                           {
                               UserId = user.UserId,
                               Name = user.Name,
                               Surname = user.Surname,
                           },
                           Notes = carPool.Notes,
                           Active = carPool.Active
                       }).ToListAsync();
        return results;
    }

    public async Task SaveCarPool(CarPool carPool)
    {
       await _context.AddAsync(carPool);
       await _context.SaveChangesAsync();
    }
}
