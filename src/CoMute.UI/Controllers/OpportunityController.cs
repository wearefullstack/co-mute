using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.UI.Controllers
{
    public class OpportunityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int Id)
        {
            return View();
        }
    }
}
