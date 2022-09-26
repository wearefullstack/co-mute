using CoMute.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoMute.Interfaces
{
  public interface IUserCarPoolService
  {
    List<UserCarPool> GetUserCarPools(int userId);
    Task<int> JoinCarPoolAsync(int carPoolId, int userId);
    Task<int> LeaveCarPoolAsync(int carPoolId, int userId);
  }
}
