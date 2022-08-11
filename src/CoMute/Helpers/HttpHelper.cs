using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Controllers.Web.Helpers
{
    public class HttpHelper<T>
    {
        public async Task<T> GetRestServiceDataAsync(string serviceAddress)
        {
            var baseUrl = "http://localhost:59598/api/" + serviceAddress;
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            var response = await client.GetAsync(client.BaseAddress);
            response.EnsureSuccessStatusCode();
            var jsonResult = response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(jsonResult.Result);
            return result;
        }

        public async Task<T> PostRestServiceDataAsync(string serviceAddress, object data)
        {
            var baseUrl = "http://localhost:59598/api/" + serviceAddress;
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            var content = JsonConvert.SerializeObject(data);
            var requestContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress, requestContent);
            response.EnsureSuccessStatusCode();
            var jsonResult = response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(jsonResult.Result);
            return result;
        }

        public async Task PutRestServiceDataAsync(string serviceAddress, object data)
        {
            var baseUrl = "http://localhost:59598/api/" + serviceAddress;
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };            
            var content = JsonConvert.SerializeObject(data);
            var requestContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(client.BaseAddress, requestContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteRestServiceDataAsync(string serviceAddress, object data)
        {
            var baseUrl = "http://localhost:59598/api/" + serviceAddress;
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            var content = JsonConvert.SerializeObject(data);
            var requestContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress, requestContent);
            response.EnsureSuccessStatusCode();
        }
    }
}