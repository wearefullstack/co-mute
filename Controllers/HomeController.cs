using Microsoft.AspNetCore.Mvc;
using Co_Mute.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Co_Mute.Data;

namespace Co_Mute.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Driver()
        {           
            return View();
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

        [HttpGet]
        public async Task<IActionResult> Owners()
        {
            var user = await (
                from u in _context.Users
                join ur in _context.UserRoles
                    on u.Id equals ur.UserId
                    join r in _context.Roles
                    on ur.RoleId equals r.Id
                select new 
                {
                    Name = u.FirstName,
                    Id = u.Id,
                    Surname = u.LastName
                   

                }).ToListAsync();

            return Json(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOppertunities()
        {
            /* var opp = await (
                 from u in _context.Oppertunities

                 select new
                 {

                     Id = u.Id,



                 }).ToListAsync();

             return Json(opp);*/

            return Ok();       
        }
    }
}