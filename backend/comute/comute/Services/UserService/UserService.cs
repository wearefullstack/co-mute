using comute.Data;
using comute.Models;
using Microsoft.EntityFrameworkCore;

namespace comute.Services.UserService;

public class UserService : IUserService
{

    private readonly DataContext _context;
    public UserService(DataContext context) => _context = context;
    public async Task<User> CurrentUser(int userId) =>
        await Task.Run(() => _context.Users.FirstOrDefault(user => user.UserId == userId));

    public async Task RegisterUser(int userId, User user)
    {
        if (_context.Users.Any(u => u.UserId == userId))
        {
            _context.ChangeTracker.Clear();
            await Task.Run(() => _context.Update(user));
        }
        else
            await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public Task<List<User>> Users() =>
        _context.Users.ToListAsync();

}
