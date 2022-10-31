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

        public static async Task<HttpResponseMessage> GetCarPool(int carpoolId)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync($"carpool/{carpoolId}");
            return response;
        }
    }
}