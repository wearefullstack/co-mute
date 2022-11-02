using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Service
{
    public static class ComuteService
    {
        private static readonly HttpClient _client = ServiceSingleton.GetInstance;

        public static async Task<HttpResponseMessage> RegisterUser(RegistrationRequest registrationRequest)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string content = JsonConvert.SerializeObject(registrationRequest);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("user/add", stringContent);
            return response;
        }

        public static async Task<HttpResponseMessage> LoginUser(LoginRequest loginRequest)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string content = JsonConvert.SerializeObject(loginRequest);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("authentication", stringContent);
            return response;
        }

        public static async Task<HttpResponseMessage> GetCarPoolMemberships(int userId)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync($"user/{userId}/carpool/memberships");
            return response;
        }

        public static async Task<HttpResponseMessage> JoinCarPool(CarPoolMembership membership)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string content = JsonConvert.SerializeObject(membership);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync($"carpool/membership/add", stringContent);
            return response;
        }

        public static async Task<HttpResponseMessage> GetCarPool(int carpoolId)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync($"carpool/{carpoolId}");
            return response;
        }

        public static async Task<HttpResponseMessage> GetCarPools()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync("carpools");
            return response;
        }

        public static async Task<HttpResponseMessage> GetAvailableCarPools(int userId)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync($"user/{userId}/carpools/available");
            return response;
        }

        public static async Task<HttpResponseMessage> CreateCarPool(CarPoolDto carPoolDto)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string content = JsonConvert.SerializeObject(carPoolDto);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("carpool/add", stringContent);
            return response;
        }

        public static async Task<HttpResponseMessage> GetUserCarPools(int userId)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync($"user/{userId}/carpools");
            return response;
        }

        public static async Task<HttpResponseMessage> Filter(int userId, string origin = "", string destination = "")
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync($"user/{userId}/carpools/filter?origin={origin}&destination={destination}");
            return response;
        }

        public static async Task<HttpResponseMessage> LeaveCarPool(int userId, int membershipId)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = await _client.DeleteAsync($"user/{userId}/carpool/membership/{membershipId}");
            return responseMessage;
        }

        public static async Task<HttpResponseMessage> GetUser(int userId)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = await _client.GetAsync($"user/{userId}");
            return responseMessage;
        }

        public static async Task<HttpResponseMessage> EditUser(EditDto editDto)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string content = JsonConvert.SerializeObject(editDto);
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("user/update", stringContent);
            return response;
        }
    }
}