using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoMuteCore.Data;
using CoMuteCore.Models;

namespace CoMuteCore.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _ctx;
        public UserService(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateUserAsync(User User)
        {
           await _ctx.User.AddAsync(User);
           await _ctx.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(int Id)
        {
           return await _ctx.User.FindAsync(Id);
        }

        public Task<User> ReadCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}