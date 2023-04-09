using CoMute.UI.Models;
using CoMute.UI.Models.Authentication;
using CoMute.UI.Services.Opportunity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOpportunityService opportunityService;

        public HomeController(ILogger<HomeController> logger,IOpportunityService opportunityService)
        {
            _logger = logger;
            this.opportunityService = opportunityService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var data = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(data))
                return Redirect("~/Account/Login");

           var converted = JsonConvert.DeserializeObject<AuthenticationModel>(data); 


            var displayOpportunities = await opportunityService.GetOpportunityByUserAsync(converted.UserId,converted.Token);
            return View(displayOpportunities);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
