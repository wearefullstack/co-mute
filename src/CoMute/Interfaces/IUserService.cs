using CoMute.Models;
using CoMute.Models.Requests;
using System.Threading.Tasks;

namespace CoMute.Interfaces
{
  public interface IUserService
  {
    User Authenticate(string username, string password);
    User GetUser(int userId);
    User RegisterUser(User user);
    Task<int> UpdateUserAsync(UserUpdateRequest user);
  }
}
