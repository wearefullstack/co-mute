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
        public async Task<IActionResult> DeleteCarpoolOppertunityUser([FromQuery] Guid id)
        {
            var opp = await _context.Listings.SingleOrDefaultAsync(x => x.Id == id);

            if (opp != null)
            {
                 _context.Listings.Remove(opp);

                 await _context.SaveChangesAsync();

                 return Json(opp);

            }

            return BadRequest("Opportunity not found");
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
             var opp = await (
                 from u in _context.Oppertunities
                 select new
                 {

                     Id = u.Id,
                     DateCreated = u.CreateDate.ToString("yyyy MMMM dd"),
                     ExpectedArrival = u.ExpectedArrival,
                     DepartTime = u.DepartTime,
                     Origin = u.Origin


                 }).ToListAsync();

             return Json(opp);

        }
        [HttpGet]
        public async Task<IActionResult> GetCurrentOppertunitiesListing([FromQuery] Guid id)
        {

            var opp = await (
                from u in _context.Listings
                join op in _context.Oppertunities
                on u.OpertunityId equals op.Id
                join user in _context.Users
                on u.UserId equals user.Id
                where u.OpertunityId == id
                select new
                {
                    Id = u.Id,
                    Name= user.FirstName + " " +user.LastName,
                    DateJoined = u.UserJoinDate.ToString("yyyy MMMM dd"),

                }).ToListAsync();

            return Json(opp);

        }
        public async Task<IActionResult> CurrentOppertunity([FromQuery] Guid id)
        {
            var opp = await _context.Oppertunities.SingleOrDefaultAsync(x => x.Id == id);

            if (opp != null)
            {
                
                return View(new CurrentOppertunityPostModal()
                {
                    Id = opp.Id,
                    Origin = opp.Origin,
                    NumberOfSeats = opp.NumberOfSeats
                    
                });

            }
            else
            {
                return View("Error");
            }


        }
        public async Task<IActionResult> CreateOppertunity([FromBody] CreateOppertunityPostModal modal)
        {
            if (ModelState.IsValid)
            {
              var nowdate = DateTime.Now;
              var newOpp = new Oppertunities()
              {
                  Id = Guid.NewGuid(),   
                  CreateDate = nowdate,
                  Origin = modal.Origin,
                  /*DepartTime = modal.DepartTime,
                  ExpectedArrival = modal.ExpectedArrival, */
                  Notes = modal.Notes,
                  /*Monday = modal.Monday,
                  Tuesday = modal.Tuesday,
                  Wednesday = modal.Wednesday,
                  Thursday = modal.Thursday,
                  Friday = modal.Friday,
                  Saturday = modal.Saturday,
                  Sunday = modal.Sunday,*/
              };

                            
               await _context.Oppertunities.AddAsync(newOpp);
               await _context.SaveChangesAsync();
               return Json(newOpp);
                 
            }

            return BadRequest("Modal not found");
        }

        public async Task<IActionResult> AddUserToCurrentOppertunity([FromQuery] Guid id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                var opportunity = await _context.Oppertunities.SingleOrDefaultAsync(x => x.Id == id);

                if (opportunity != null)
                {

                    var newUserList = await _context.Listings.SingleOrDefaultAsync(x => x.UserId == user.Id && x.OpertunityId == opportunity.Id);
                    if (newUserList == null)
                    {
                        var numUserList = await _context.Listings.Where(x => x.OpertunityId == opportunity.Id).ToListAsync();
                        if (opportunity.NumberOfSeats > numUserList.Count)

                        {

                            var date = DateTime.Now;

                            var list = new Listing()
                            {
                                Id = Guid.NewGuid(),
                                UserId = user.Id,
                                OpertunityId = id,
                                UserJoinDate = date
                            };

                            await _context.Listings.AddAsync(list);
                            await _context.SaveChangesAsync();
                            return Json(list);


                        }


                        return BadRequest("There are no seats available");

                    }

                    return BadRequest("You are already in this carpool");
                  

                }
                return BadRequest("Opportunity not found");


            }

            return BadRequest("User not found");
            
        }
    }
}