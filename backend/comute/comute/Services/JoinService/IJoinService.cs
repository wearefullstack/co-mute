using comute.Models;

namespace comute.Services.JoinService;

public interface IJoinService
{
    Task<List<JoinInfo>> JoinedCarPools(int userId);
    Task<List<JoinCarPool>> AllCarPoolsJoined();
    Task<bool> SaveJoinCarPool(int carPoolId, int userId);
    Task LeaveCarPoolOpportunity(int joinId);
}
