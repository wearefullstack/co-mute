using comute.Data;
using comute.Models;

namespace comute.Services.JoinService;

public class JoinService : IJoinService
{
    private readonly DataContext _context;

    public JoinService(DataContext context) =>_context = context;


    public async Task<List<JoinCarPool>> AllCarPoolsJoined()
    {
        throw new NotImplementedException();
    }

    public async Task<List<JoinInfo>> JoinedCarPools(int userId)
    {
        throw new NotImplementedException();
    }

    public Task LeaveCarPoolOpportunity(int joinId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveJoinCarPool(int carPoolId, int userId)
    {
        throw new NotImplementedException();
    }
}
