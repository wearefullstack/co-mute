using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoMuteCore.Models;

namespace CoMuteCore.Services
{
    public interface IUserService
    {
        public Task<User> GetUserAsync(int Id);
        public Task CreateUserAsync(User User);
        public Task<User> ReadCurrentUser();
    }
}