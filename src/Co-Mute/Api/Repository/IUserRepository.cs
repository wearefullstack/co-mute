using Co_Mute.Api.Models;
using Co_Mute.Api.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Co_Mute.Api.Repository
{
    public interface IUserRepository
    {
        public Task<FunctionCommandUser> LoginUser(UserLogin userLogin);
        public Task<int> UpdateUserDetails(UpdateUser updateUser, int id);
        public Task RegisterNewUser(UserRegisterDto oCreateUser);
        public string CreateToken(UserLogin oUserLogin);
    }
}
