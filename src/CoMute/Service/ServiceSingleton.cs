using System.Net.Http;

namespace CoMute.Web.Service
{
    public sealed class ServiceSingleton
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private ServiceSingleton()
        {
        }

        public static HttpClient GetInstance
        {
            get
            {
                return _httpClient;
            }
        }
    }
}