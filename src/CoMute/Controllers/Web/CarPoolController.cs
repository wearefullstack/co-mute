using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class CarPoolController : Controller
    {
        // GET: CarPool
        public async Task<ActionResult> Index()
        {
            var adapter = new Adapter.Adapter(new HttpClient());
            var res = await adapter.GetCarPools();

            return View(res.AsEnumerable());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CarPoolRequest model)
        {
            int userId = Int32.Parse( User.Identity.Name);
            model.OwnerId = userId;
            var adapter = new Adapter.Adapter(new HttpClient());
            var res = await adapter.CreateCarPool(model);

            return View(res);
        }

        // Joined

        public async Task<ActionResult> Joined()
        {
            var adapter = new Adapter.Adapter(new HttpClient());
            var res = await adapter.GetJoinedCarPools(LoggedInUser());
            return View(res.AsEnumerable());
        }

        public async Task<ActionResult> Available()
        {
            var adapter = new Adapter.Adapter(new HttpClient());
            var res = await adapter.GetAvailableCarPools(LoggedInUser());
            return View(res.AsEnumerable());
        }

        // join
        public async Task<ActionResult> JoinPool(int id)
        {
            var model = new UserPool() { poolId = id, userId = LoggedInUser() };

            var adapter = new Adapter.Adapter(new HttpClient());
            var res = await adapter.JoinCarPool(model);
            return RedirectToAction("Joined", "CarPool");
        }

        // Leave
        public async Task<ActionResult> LeavePool(int id)
        {
            var model = new UserPool() { poolId = id, userId = LoggedInUser() };

            var adapter = new Adapter.Adapter(new HttpClient());
            var res = await adapter.LeaveCarPool(model);
            return RedirectToAction("Available", "CarPool");
        }

        private int  LoggedInUser()
        {
            return Int32.Parse(User.Identity.Name);
        }
    }
}