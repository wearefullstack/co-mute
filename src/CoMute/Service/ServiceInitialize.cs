using System;
using System.Net.Http;

namespace CoMute.Web.Service
{
    public static class ServiceInitialize
    {
        public static void Init(string baseUrl)
        {
            HttpClient ServiceClient = ServiceSingleton.GetInstance;
            ServiceClient.BaseAddress = new Uri(baseUrl);
        }
    }
}