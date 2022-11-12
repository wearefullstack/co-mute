using comute.Models;
using ErrorOr;

namespace comute.Services.JoinService;

public interface IJoinService
{
    Task<List<JoinInfo>> JoinedCarPools(int userId);
    Task<List<JoinCarPool>> AllCarPoolsJoined();
    Task<bool> SaveJoinCarPool(int carPoolId, int userId, JoinCarPool joinCarPool);
    Task LeaveCarPoolOpportunity(int joinId);
}
