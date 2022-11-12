using comute.Models;

namespace comute.Services.UserService;

public interface IUserService
{
    Task<List<User>> Users();
    Task<User> CurrentUser(int userId);
    Task RegisterUser(int userId, User user); 
}
