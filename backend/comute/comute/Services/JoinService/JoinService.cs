using comute.Data;
using comute.Models;
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
                       join user in _context.Users
                       on joinedPool.UserId equals user.UserId
                       join carPool in _context.CarPools
                       on joinedPool.CarPoolId equals carPool.CarPoolId 
                       where carPool.Owner == user.UserId 
                       && joinedPool.UserId == user.UserId 
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
        var joinedCarPool =await _context.JoinCarPools.SingleAsync(join => join.UserId == joinId);
        _context.Remove(joinedCarPool);
        _context.SaveChanges();
    }

    public Task<bool> SaveJoinCarPool(int carPoolId, int userId)
    {
        throw new NotImplementedException();
    }
}
