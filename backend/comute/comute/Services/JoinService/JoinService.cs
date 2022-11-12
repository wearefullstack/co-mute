using comute.Data;
using comute.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace comute.Services.JoinService;

public class JoinService : IJoinService
{
    private readonly DataContext _context;
    public JoinService(DataContext context) =>_context = context;
    public Task<List<JoinCarPool>> AllCarPoolsJoined() =>
         _context.JoinCarPools.ToListAsync();

    public Task<List<JoinInfo>> JoinedCarPools(int userId)
    {
        var results = (from joinedPool in _context.JoinCarPools
                       join carPool in _context.CarPools
                       on joinedPool.CarPoolId equals carPool.CarPoolId
                       join user in _context.Users
                       on carPool.Owner equals user.UserId 
                       where joinedPool.UserId == userId
                       select new JoinInfo()
                       { 
                           JoinId = joinedPool.JoinId,
                           UserId = joinedPool.UserId,
                           User = new()
                           {
                               UserId = user.UserId,
                               Name = user.Name,
                               Surname = user.Surname
                           },
                           CarPoolId = joinedPool.CarPoolId,
                           CarPools = new List<CarPool>() { carPool },
                           JoinedOn = joinedPool.JoinedOn
                       }).ToListAsync();
        return results;
    }

    public async Task LeaveCarPoolOpportunity(int joinId)
    
    {
        var joinedCarPool = await _context.JoinCarPools.SingleAsync(join => join.JoinId == joinId);
        _context.Remove(joinedCarPool);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> SaveJoinCarPool(int carPoolId, int userId, JoinCarPool joinCarPool)
    {
        bool result = false;
        bool isOverlapping = false;
        var carPoolToJoin = _context.CarPools.Single(join => join.CarPoolId == carPoolId);
        int toJoinCarPoolCount = _context.JoinCarPools.Count(c => c.CarPoolId == carPoolId);
        if(carPoolToJoin.DepartureTime <= joinCarPool.JoinedOn
           && carPoolToJoin.ExpectedArrivalTime <= joinCarPool.JoinedOn)
        {
            isOverlapping = true;
        }
        if (!isOverlapping)
        {
            if (carPoolToJoin.AvailableSeats > toJoinCarPoolCount)
            {
                result = false;
                await _context.AddAsync(joinCarPool);
                await _context.SaveChangesAsync();
            }
            else
                result = true;
        }
        return result;
    }
}
