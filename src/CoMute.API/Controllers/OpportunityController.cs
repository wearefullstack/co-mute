using CoMute.API.Models.Opportunity;
using CoMute.API.Services.Opportunity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;
        public OpportunityController(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        [AllowAnonymous]
        [HttpGet("GetOpportunity")]
        public async Task<ActionResult<IEnumerable<SearchOpportunityModel>>> GetOpportunityAsync()
        {
            var result = await _opportunityService.GetOpportunityAsync();
            return result.ToList();
        }

        [AllowAnonymous]
        [HttpGet("GetOpportunityByUser")]
        [Authorize(Roles = "User,Lead,LeadUser")]
        public async Task<ActionResult<IEnumerable<SearchOpportunityModel>>> GetOpportunityByUserAsync(string userId)
        {
            if(string.IsNullOrEmpty(userId))
               return BadRequest($"Invalid user.");

            var result = await _opportunityService.GetOpportunityByUserAsync(userId);
            return result.ToList();
        }

        [HttpPost("joinOpportunity")]
        [Authorize(Roles = "User,Lead,LeadUser")]
        public async Task<ActionResult> JoinOpportunityAsync(JoinOpportunityModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();

                if (errors.Any())
                {
                    return BadRequest($"Ensure to enter all the required data and is in the correct format.");
                }
            }

            var result = await _opportunityService.JoinOpportunityAsync(model);
            return Ok(result);
        }

        [HttpPost("leaveOpportunity")]
        [Authorize(Roles = "User,Lead,LeadUser")]
        public async Task<ActionResult> LeaveOpportunityAsync(LeaveOpportunityModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();

                if (errors.Any())
                {
                    return BadRequest($"Ensure to enter all the required data and is in the correct format.");
                }
            }

            var result = await _opportunityService.LeaveOpportunityAsync(model);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("SearchOpportunity")]
        public async Task<ActionResult<IEnumerable<SearchOpportunityModel>>> SearchOpportunitysAsync(SearchParameters search)
        {
            var result = await _opportunityService.SearchOpportunitysAsync(search);
            return result.ToList();
        }

        [HttpPost("registerOpportunity")]
        [Authorize(Roles = "Lead")]
        public async Task<ActionResult> RegisterOpportunityAsync(RegisterOpportunityModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();

                if (errors.Any())
                {
                    return BadRequest($"Ensure to enter all the required data and is in the correct format.");
                }
            }

            var result = await _opportunityService.RegisterOpportunityAsync(model);
            return Ok(result);
        }
    }
}
