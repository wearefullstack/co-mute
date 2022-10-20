using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoMute.BE;
using CoMute.BL;
using CoMute.Web.Models;
using Accessibility;
using System.Threading.Tasks;

namespace CoMute.Web.Controllers.Web
{
    public class CarPoolController : Controller
    {
        // GET: CarPool
        private CarPoolLogic _carPoolLogic = new CarPoolLogic();
        private JoinLeaveCarPoolLogic _joinLeaveCarPool = new JoinLeaveCarPoolLogic();

        [HttpGet]
        public ActionResult GetAllCarPools()
        {
            //CarPool model = new CarPool();
            var carpoolList= _carPoolLogic.GetAllCarPools();

            //TODO Return model
            return View();
        }

        [HttpGet]
        public ActionResult SearchCarPools(string Serach=null)
        {
            //CarPool model = new CarPool();
            var carpoolList = _carPoolLogic.CarPoolSearch(Serach);

            //TODO Return model
            return View();
        }

        [HttpGet]
        public ActionResult GetCarPoolByUserId(int UserId)
        {
            _carPoolLogic.GetCarPoolByUserId(UserId);

            //TODO Return model
            return View();

        }

        public bool CheckOverLap(DateTime DepartureTime, DateTime ExpectedArrivalTim, string Destination, DateTime DateCreated, int UserId)
        {
           var check= _carPoolLogic.Overlaps(DepartureTime, ExpectedArrivalTim, Destination, DateCreated, UserId);
           return check;
        }

        [HttpPost]
        public async Task AddCarPool(CarPool carPool)
        {
            _carPoolLogic.AddCarPool(carPool);
         
        }

        [HttpPost]
        public async Task JoinLeaveCarPool(JoinLeaveCarPool joinLeaveCarPool)
        {
            _joinLeaveCarPool.AddJoinLeaveCarPool(joinLeaveCarPool);
            
        }

        [HttpPost]
        public async Task ChangeCarPoolSatus(int JoinLeaveCarPoolId,bool IsActive )
        {
            _joinLeaveCarPool.ChangeStatusJoinLeaveCarPool(JoinLeaveCarPoolId, IsActive);

        }
    }
}
