using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CoMute.UI.Helpers
{
    public class APIHelper
    {
        public static HttpClient ApiClient { get; set; }
        private readonly IConfiguration configuration;

        public APIHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private string GetAPIUrl() => configuration["ConnectionStrings:APIUrl"].ToString();
        public void InitializeClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            ApiClient = new HttpClient(clientHandler);

            var domain = GetAPIUrl();
            ApiClient.BaseAddress = new Uri(domain);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }
    }
}
