using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoMute.Web.Models.Dto;
using Newtonsoft.Json;

namespace CoMute.Web.Adapter
{
    public  class Adapter
    {
        static HttpClient client = new HttpClient();

        public Adapter(HttpClient _client)
        {
            client = _client;
            client.BaseAddress = new Uri("http://localhost:59598/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
       

        // car pool
        public  async Task<int> CreateCarPool(CarPoolRequest model)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/CarPool/add", model);
            var res1 = await response.Content.ReadAsStringAsync();
            var res2 =  JsonConvert.DeserializeObject<int>(res1);

            return res2;
        }

        public async Task<List<CarPoolRequest>> GetCarPools()
        {
            HttpResponseMessage response = await client.GetAsync("/CarPool/GetCarPools");
            var resString = await response.Content.ReadAsStringAsync();
            var resValue = JsonConvert.DeserializeObject<List<CarPoolRequest>>(resString);
            return resValue;
        }

        public async Task<CarPoolRequest> GetCarPools(int id)
        {
            HttpResponseMessage response = await client.GetAsync("/CarPool/GetCarPoolById?Id=" + id);
            var resString = await response.Content.ReadAsStringAsync();
            var resValue = JsonConvert.DeserializeObject<CarPoolRequest>(resString);
            return resValue;
        }

        //Joined Car pools
        public async Task<List<CarPoolRequest>> GetJoinedCarPools(int id)
        {
            HttpResponseMessage response = await client.GetAsync("/CarPool/GetJoinedCarPools?Id=" + id);
            var resString = await response.Content.ReadAsStringAsync();
            var resValue = JsonConvert.DeserializeObject<List<CarPoolRequest>>(resString);
            return resValue;
        }

        //Available Car pools
        public async Task<List<CarPoolRequest>> GetAvailableCarPools(int id)
        {
            HttpResponseMessage response = await client.GetAsync("/CarPool/GetAvailableCarPools?Id=" + id);
            var resString = await response.Content.ReadAsStringAsync();
            var resValue = JsonConvert.DeserializeObject<List<CarPoolRequest>>(resString);
            return resValue;
        }

        //join pools
        public async Task<int> JoinCarPool(UserPool model)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/CarPool/JoinPool" , model);
            var resString = await response.Content.ReadAsStringAsync();
            var resValue = JsonConvert.DeserializeObject<int>(resString);
            return resValue;
        }

        //Leave pools
        public async Task<int> LeaveCarPool(UserPool model)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/CarPool/LeavePool", model);
            var resString = await response.Content.ReadAsStringAsync();
            var resValue = JsonConvert.DeserializeObject<int>(resString);
            return resValue;
        }


        // Account.

        public async Task<int> Login(LoginRequest model)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/Account/Login", model);
            var resString = await response.Content.ReadAsStringAsync();
            var resValue = JsonConvert.DeserializeObject<int>(resString);
            return resValue;
        }

        public async Task<int> UpdateProfileAccount(ProfileViewModel model)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/Account/UpdateProfile", model);
            var resString = await response.Content.ReadAsStringAsync();
            var resValue = JsonConvert.DeserializeObject<int>(resString);
            return resValue;
        }

        public async Task<ProfileViewModel> GetUserProfileById(int id)
        {
            HttpResponseMessage response = await client.GetAsync("/Account/GetUserProfileById?Id=" + id);
            var resString = await response.Content.ReadAsStringAsync();
            var resValue = JsonConvert.DeserializeObject<ProfileViewModel>(resString);
            return resValue;
        }
    }
}