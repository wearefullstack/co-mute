using Microsoft.AspNetCore.Mvc;
using Co_Mute.Models;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Co_Mute.Data;
using Co_Mute.Models.AdministrationViewModels;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Presentation;

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
        public async Task<IActionResult> All()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {

                return View(new ProfileViewModel()
                {

                    FirstName = user.FirstName + " " + user.LastName,

                });

            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> AllOpportunities()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {

                return View(new ProfileViewModel()
                {
                   
                    FirstName = user.FirstName + " " + user.LastName,

                });

            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult MyJoinedOpportunities()
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

        [HttpDelete]
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
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var opp = await (
                 from u in _context.Oppertunities
                 where u.OwnerId == currentUser.Id
                 select new
                 {

                     Id = u.Id,
                     DateCreated = u.CreateDate.ToString("yyyy MMMM dd"),
                     ArrivalTime = u.ExpectedArrival,
                     DepartTime = u.DepartTime,
                     Origin = u.Origin


                 }).ToListAsync();

             return Json(opp);

        }
        [HttpGet]
        public async Task<IActionResult> AllOppertunities()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var opp = await (
                from u in _context.Oppertunities
                join r in _context.Users
                    on u.OwnerId equals r.Id
                select new
                {

                    Id = u.Id,
                    DateCreated = u.CreateDate.ToString("yyyy MMMM dd"),
                    ArrivalTime = u.ExpectedArrival,
                    DepartTime = u.DepartTime,
                    Driver = r.FirstName + " " + r.LastName,


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
        [HttpGet]
        public async Task<IActionResult> GetMyJoinedOppoertunities()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var opp = await (
                from u in _context.Listings
                join user in _context.Users
                    on u.UserId equals user.Id
                join op in _context.Oppertunities
                    on u.OpertunityId equals op.Id
                where u.UserId == currentUser.Id
                select new
                {
                    Id = u.Id,
                    Name = user.FirstName + " " + user.LastName,
                    DateJoined = u.UserJoinDate.ToString("yyyy MMMM dd"),
                    Destination = op.Destination,
                    ArrivalTime = op.ExpectedArrival,
                    DepartTime = op.DepartTime,

                }).ToListAsync();

            return Json(opp);

        }
        public async Task<IActionResult> CurrentOppertunity([FromQuery] Guid id)
        {
            var opp = await _context.Oppertunities.SingleOrDefaultAsync(x => x.Id == id);

            if (opp != null)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == opp.OwnerId);
                if (user != null)
                {
                    return View(new CurrentOppertunityPostModal()
                    {
                        Id = opp.Id,
                        Origin = opp.Origin,
                        NumberOfSeats = opp.NumberOfSeats,
                        Owner =user.FirstName + " " + user.LastName,


                    });
                }

                return BadRequest("User not found");
              

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
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var nowdate = DateTime.Now;


                  var listMyCars = await _context.Oppertunities.Where(X => X.OwnerId == user.Id).ToListAsync();

                  if (listMyCars.Count > 0)
                  {
                      foreach (var list in listMyCars)
                      {
                          DateTime modalDepartTime = DateTime.Parse(modal.DepartTime);
                          DateTime modalArrivalTime = DateTime.Parse(modal.ArrivalTime);

                          DateTime DepartTime = DateTime.Parse(list.DepartTime);
                          DateTime ArrivalTime = DateTime.Parse(list.ExpectedArrival);
                          if (DepartTime < modalDepartTime || ArrivalTime < modalArrivalTime)
                          {
                              return BadRequest(
                                  "You cannot book this session as you are already booked somewhere else");
                          }
                          {
                              var newOpp = new Oppertunities()
                              {
                                  Id = Guid.NewGuid(),
                                  CreateDate = nowdate,
                                  Origin = modal.Origin,
                                  OwnerId = user.Id,
                                  Notes = modal.Notes,
                                  Destination = modal.Destination,
                                  NumberOfSeats = modal.NumSeats,
                                  Monday = modal.Monday,
                                  Tuesday = modal.Tuesday,
                                  Wednesday = modal.Wednesday,
                                  Thursday = modal.Thursday,
                                  Friday = modal.Friday,
                                  Saturday = modal.Saturday,
                                  Sunday = modal.Sunday,
                                  DepartTime = modal.DepartTime,
                                  ExpectedArrival = modal.ArrivalTime,
                              };


                              await _context.Oppertunities.AddAsync(newOpp);
                              await _context.SaveChangesAsync();

                              return Json(new
                              {
                                  Id = newOpp.Id,
                                  CreatedDate = newOpp.CreateDate,
                                  ArrivalTime = newOpp.ExpectedArrival,
                                  DepartTime = newOpp.DepartTime,
                                  Origin = newOpp.Origin,

                              });
                          }
                    }

                  }
                  else
                  {
                        var newOpp = new Oppertunities()
                        {
                            Id = Guid.NewGuid(),
                            CreateDate = nowdate,
                            Origin = modal.Origin,
                            OwnerId = user.Id,
                            Notes = modal.Notes,
                            Destination = modal.Destination,
                            NumberOfSeats = modal.NumSeats,
                            Monday = modal.Monday,
                            Tuesday = modal.Tuesday,
                            Wednesday = modal.Wednesday,
                            Thursday = modal.Thursday,
                            Friday = modal.Friday,
                            Saturday = modal.Saturday,
                            Sunday = modal.Sunday,
                            DepartTime = modal.DepartTime,
                            ExpectedArrival = modal.ArrivalTime,
                        };


                        await _context.Oppertunities.AddAsync(newOpp);
                        await _context.SaveChangesAsync();

                        return Json(new
                        {
                            Id = newOpp.Id,
                            CreatedDate = newOpp.CreateDate,
                            ArrivalTime = newOpp.ExpectedArrival,
                            DepartTime = newOpp.DepartTime,
                            Origin = newOpp.Origin,

                        });
                  }
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