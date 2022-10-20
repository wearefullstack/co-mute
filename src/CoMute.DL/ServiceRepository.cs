using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CoMute.DL
{
    public class ServiceRepository
    {
        public HttpClient Client { get; set; }
        public ServiceRepository()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"].ToString());
        }
        public HttpResponseMessage GetResponse(string url)
        {

            return Client.GetAsync(url).Result;
        }
        public HttpResponseMessage PutResponse(string url, object model)
        {
            var serializer = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(serializer, Encoding.UTF8, "application/json");
            return Client.PutAsync(url, stringContent).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            var serializer = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(serializer, Encoding.UTF8, "application/json");
            return Client.PutAsync(url, stringContent).Result;
        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }
    }
}
