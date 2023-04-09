using CoMute.UI.Helpers;
using CoMute.UI.Models.Authentication;
using CoMute.UI.Models.Tokens;
using CoMute.UI.Models.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<ProfileModel> GetUserProfileAsync(string userId,string token)
        {
            APIHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await APIHelper.ApiClient.GetAsync(APIHelper.ApiClient.BaseAddress + $"user/GetUserProfile?userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var profile = JsonConvert.DeserializeObject<ProfileModel>(content);
                return profile;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
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

        public async Task<string> UpdateUserProfileAsync(ProfileModel profileModel)
        {
            string result = "FAILED.Update user Profile";
            var json = JsonConvert.SerializeObject(profileModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            APIHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", profileModel.Token);
            response = await APIHelper.ApiClient.PostAsync(APIHelper.ApiClient.BaseAddress + "user/UpdateUserProfile", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<string>(data);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                result = "UNAUTHORIZED.Update failed. Unauthorized User";

            return result;
        }
    }
}
