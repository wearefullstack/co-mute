using CoMute.UI.Helpers;
using CoMute.UI.Models.Authentication;
using CoMute.UI.Models.Tokens;
using CoMute.UI.Models.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.UI.Services.Users
{
    public class UserService:IUserService
    {
        private HttpResponseMessage response;
        public UserService(HttpResponseMessage response)
        {
            this.response = response;
        }

        public Task<string> AddRoleAsync(AddRoleModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            AuthenticationModel authenticationModel = new();
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await APIHelper.ApiClient.PostAsync(APIHelper.ApiClient.BaseAddress + "user/token" ,content);

            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                authenticationModel = JsonConvert.DeserializeObject<AuthenticationModel>(data);
            }
            return authenticationModel;
        }

        public Task<ProfileModel> GetUserProfileAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            string result = "FAILED.";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await APIHelper.ApiClient.PostAsync(APIHelper.ApiClient.BaseAddress + "user/register", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<string>(data);
            }
            return result;
        }

        public Task<string> UpdateUserProfileAsync(ProfileModel profileModel)
        {
            throw new NotImplementedException();
        }
    }
}
