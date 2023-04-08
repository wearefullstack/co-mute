using CoMute.UI.Models.Opportunity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.UI.Services.Opportunity
{
    public interface IOpportunityService
    {
        Task<string> RegisterOpportunityAsync(RegisterOpportunityModel model);

        Task<string> JoinOpportunityAsync(JoinOpportunityModel model);
        Task<string> LeaveOpportunityAsync(LeaveOpportunityModel leaveOpportunity);

        Task<IEnumerable<SearchOpportunityModel>> GetOpportunityAsync();
        Task<IEnumerable<SearchOpportunityModel>> GetOpportunityByUserAsync(string userId, string token);

        Task<IEnumerable<SearchOpportunityModel>> SearchOpportunitysAsync(SearchParameters search);
    }
}
