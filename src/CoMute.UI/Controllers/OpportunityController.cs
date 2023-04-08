using CoMute.UI.Models.Authentication;
using CoMute.UI.Models.Opportunity;
using CoMute.UI.Services.Opportunity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.UI.Controllers
{
    public class OpportunityController : Controller
    {
        private readonly IOpportunityService opportunityService;

        public OpportunityController(IOpportunityService opportunityService)
        {
            this.opportunityService = opportunityService;
        }
        public IActionResult RegisterOpportunity()
        {
            var data = HttpContext.Session.GetString("Roles");
            if (string.IsNullOrEmpty(data))
                return Redirect("~/Account/Login");

            var roles = JsonConvert.DeserializeObject<List<string>>(data);

            var validateRoles = roles.Where(x => x.ToString().ToLower().Contains("Lead".ToLower()) || x.ToString().ToLower().Contains("LeadUser".ToLower())).Count();
            if(validateRoles <= 0)
                return Redirect("~/Account/Login");

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> RegisterOpportunityAsync(RegisterOpportunityModel registerOpportunity)
        {
            if (string.IsNullOrEmpty(registerOpportunity.Notes))
                registerOpportunity.Notes = string.Empty;

            if (!ModelState.IsValid)
            {
                TempData["RegisterFailed"] = $"Registration Failed due to incorrect form values";
                TempData["RegisterSuccess"] = null;
                return Redirect("~/Opportunity/RegisterOpportunity");
            }

            registerOpportunity.IsLeader = true;
            registerOpportunity.JoinedDate = DateTime.Now;
            registerOpportunity.CreatedDate = DateTime.Now;

            var data = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(data))
                return Redirect("~/Account/Login");

            var authenticatedData = JsonConvert.DeserializeObject<AuthenticationModel>(data);

            if (string.IsNullOrEmpty(authenticatedData.UserId))
                return Redirect("~/Account/Login");

            registerOpportunity.UserId = authenticatedData.UserId;
            registerOpportunity.Token = authenticatedData.Token;

            registerOpportunity.OpportunityName = $"Opportunity-{Guid.NewGuid()}";
            var register = await opportunityService.RegisterOpportunityAsync(registerOpportunity);
            if (register.Contains("FAILED."))
            {
                TempData["RegisterFailed"] = $"{register}";
                TempData["RegisterSuccess"] = null;
                return Redirect("~/Opportunity/RegisterOpportunity");
            }
            else
            {
                TempData["RegisterSuccess"] = $"{register}";
                TempData["RegisterFailed"] = null;
            }

            return Redirect("~/Home/Index");
        }
    }
}
