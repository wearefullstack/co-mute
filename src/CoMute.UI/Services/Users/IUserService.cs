using CoMute.UI.Models.Authentication;
using CoMute.UI.Models.Tokens;
using CoMute.UI.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.UI.Services.Users
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<ProfileModel> GetUserProfileAsync(string userId);
        Task<string> UpdateUserProfileAsync(ProfileModel profileModel);
    }
}
