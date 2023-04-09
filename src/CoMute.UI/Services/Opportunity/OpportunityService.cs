using CoMute.UI.Helpers;
using CoMute.UI.Models.Opportunity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.UI.Services.Opportunity
{
    public class OpportunityService: IOpportunityService
    {
        private HttpResponseMessage response;

        public OpportunityService(HttpResponseMessage response)
        {
            this.response = response;
        }

        public async Task<IEnumerable<SearchOpportunityModel>> GetOpportunityAsync()
        {

            response = await APIHelper.ApiClient.GetAsync(APIHelper.ApiClient.BaseAddress + "Opportunity/GetOpportunity");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var opportunities = JsonConvert.DeserializeObject<IEnumerable<SearchOpportunityModel>>(content);
                return opportunities;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<IEnumerable<SearchOpportunityModel>> GetOpportunityByUserAsync(string userId,string token)
        {
            APIHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            response = await APIHelper.ApiClient.GetAsync(APIHelper.ApiClient.BaseAddress + $"Opportunity/GetOpportunityByUser?userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var opportunities = JsonConvert.DeserializeObject<IEnumerable<SearchOpportunityModel>>(content);
                return opportunities;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<string> JoinOpportunityAsync(JoinOpportunityModel model)
        {
            string result = "FAILED.Unable to Join this opportunity";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            APIHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);
            response = await APIHelper.ApiClient.PostAsync(APIHelper.ApiClient.BaseAddress + "Opportunity/joinOpportunity", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<string>(data);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                result = "UNAUTHORIZED.Registeration failed. Unauthorized User";

            return result;
        }

        public async Task<string> LeaveOpportunityAsync(LeaveOpportunityModel leaveOpportunity)
        {

            string result = "FAILED.Unable to Leave this opportunity";
            var json = JsonConvert.SerializeObject(leaveOpportunity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            APIHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", leaveOpportunity.Token);
            response = await APIHelper.ApiClient.PostAsync(APIHelper.ApiClient.BaseAddress + "Opportunity/leaveOpportunity", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<string>(data);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                result = "UNAUTHORIZED.Leave failed. Unauthorized User";

            return result;
        }

        public async Task<string> RegisterOpportunityAsync(RegisterOpportunityModel model)
        {
            string result = "FAILED.";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            APIHelper.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);
            response = await APIHelper.ApiClient.PostAsync(APIHelper.ApiClient.BaseAddress + "Opportunity/RegisterOpportunity", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<string>(data);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                result = "Registeration failed. Unauthorized User";

            return result;
        }

        public Task<IEnumerable<SearchOpportunityModel>> SearchOpportunitysAsync(SearchParameters search)
        {
            throw new NotImplementedException();
        }
    }
}
