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

            TempData["LeaveFailed"] = null;
            TempData["LeaveSuccess"] = null;
            TempData["JoinFailed"] = null;
            TempData["JoinSuccess"] = null; 
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
                TempData["RegisterOpportunityFailed"] = $"{register}";
                TempData["RegisterOpportunitySuccess"] = null;
                return Redirect("~/Opportunity/RegisterOpportunity");
            }
            else
            {
                TempData["RegisterOpportunitySuccess"] = $"{register}";
                TempData["RegisterOpportunityFailed"] = null;
            }

            return Redirect("~/Home/Index");
        }

        public async Task<IActionResult> CarPoolOpportunityAsync()
        {
            var data = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(data))
                return Redirect("~/Account/Login");

            var displayOpportunities = await opportunityService.GetOpportunityAsync();
            return View(displayOpportunities);
        }

        public async Task<IActionResult> JoinOpportunityAsync(int Id)
        {
            var getOpportunity = await opportunityService.GetOpportunityAsync();
            var getDetails = getOpportunity.ToList().Where(x => x.OpportunityId == Id).FirstOrDefault();
            return View(getDetails);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> JoinOpportunityAsync(SearchOpportunityModel join)
        {
            var data = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(data))
                return Redirect("~/Account/Login");

            var converted = JsonConvert.DeserializeObject<AuthenticationModel>(data);

            TempData["RegisterFailed"] = null;
            TempData["RegisterSuccess"] = null;
            TempData["RegisterOpportunitySuccess"] = null;
            TempData["RegisterOpportunityFailed"] = null;
            //get the users opportunities joined
            var displayOpportunities = await opportunityService.GetOpportunityByUserAsync(converted.UserId, converted.Token);

            //check if the user has already joined an opportunity
            var existingOpportunity = displayOpportunities.ToList().Where(x => x.OpportunityId == join.OpportunityId);

            if(existingOpportunity.Count() > 0)
            {
                TempData["JoinFailed"] = $"You have already joined this opportunity with code {existingOpportunity.FirstOrDefault().OpportunityName}";
                TempData["JoinSuccess"] = null;
                return RedirectToAction("JoinOpportunity", new
                {
                    Id = join.OpportunityId
                });
            }
            else
            {
                TempData["JoinFailed"] = null;       
            }
            JoinOpportunityModel joinOpportunity = new();
            joinOpportunity.OpportunityId = join.OpportunityId;
            joinOpportunity.UserId = converted.UserId;
            joinOpportunity.Token = converted.Token;
            joinOpportunity.IsLeader = false;
            joinOpportunity.JoinDate = DateTime.Now;


            var success = await opportunityService.JoinOpportunityAsync(joinOpportunity);
            if (success.ToLower().Contains("FAILED.".ToLower()) || success.ToLower().Contains("UNAUTHORIZED.".ToLower()))
            {
                TempData["JoinFailed"] = $"{success}";
                TempData["JoinSuccess"] = null;
                return Redirect($"~/Opportunity/JoinOpportunity/{join.OpportunityId}");
            }
            else
            {
                TempData["JoinFailed"] = null;
                TempData["JoinSuccess"] = $"{success}";
            }

            return Redirect("~/Home/Index");
        }

        public async Task<IActionResult> LeaveOpportunityAsync(int Id)
        {
            var getOpportunity = await opportunityService.GetOpportunityAsync();
            var getDetails = getOpportunity.ToList().Where(x => x.OpportunityId == Id).FirstOrDefault();
            return View(getDetails);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> LeaveOpportunityAsync(LeaveOpportunityModel leave)
        {
            var data = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(data))
                return Redirect("~/Account/Login");

            TempData["RegisterFailed"] = null;
            TempData["RegisterSuccess"] = null;
            TempData["RegisterOpportunitySuccess"] = null;
            TempData["RegisterOpportunityFailed"] = null;

            var converted = JsonConvert.DeserializeObject<AuthenticationModel>(data);

            LeaveOpportunityModel leaveOpportunity = new();
            leaveOpportunity.OpportunityId = leave.OpportunityId;
            leaveOpportunity.UserId = converted.UserId;
            leaveOpportunity.Token = converted.Token;


            var success = await opportunityService.LeaveOpportunityAsync(leaveOpportunity);
            if (success.ToLower().Contains("FAILED.".ToLower()) || success.ToLower().Contains("UNAUTHORIZED.".ToLower()))
            {
                TempData["LeaveFailed"] = $"{success}";
                TempData["LeaveSuccess"] = null;
                return Redirect($"~/Opportunity/LeaveOpportunity/{leave.OpportunityId}");
            }
            else
            {
                TempData["LeaveFailed"] = null;
                TempData["LeaveSuccess"] = $"{success}";
            }

            return Redirect("~/Home/Index");
        }
    }
}
