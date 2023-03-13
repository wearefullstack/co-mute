using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.Models;
using CoMute.Web.Models.DAL;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.Controllers.Web
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {            
            ComuteDBEntities db = new ComuteDBEntities();

            if(LoggedInUser.Id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(db.CarpoolsTables);
        }

        public ActionResult CreateCarpool()
        {
            return View();
        }

        public ActionResult ViewCarpoolCreated()
        {
            int idNum = 0;
            idNum = LoggedInUser.Id;

            if (idNum == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewCModel = new List<CarPoolDataSetToView>();

            ComuteDBEntities db = new ComuteDBEntities();
            int numCarpools = db.CarpoolsTables.Count();
            int OwnerID;

            for(int i = 0; i < numCarpools; i++)
            {
                CarpoolsTable theCarpool = db.CarpoolsTables.FirstOrDefault(x=>x.CarpoolID == i);
                if (theCarpool == null)
                {
                    OwnerID = 0;
                }
                else
                {
                    OwnerID = theCarpool.OwnerID;
                }
                
                if(OwnerID == LoggedInUser.Id)
                {
                    viewCModel.Add(new CarPoolDataSetToView
                    {
                        Name = LoggedInUser.Name,
                        Origin = theCarpool.Origin,
                        Destination = theCarpool.Destination,
                        Days = theCarpool.DaysAvailable,
                        Seats = theCarpool.AvailableSeats,
                        DepartTime = theCarpool.DepartureTime,
                        ExpectTime = theCarpool.ExpectedTime,
                        Notes = theCarpool.Notes
                    });
                }
            }

            return View(viewCModel);
        }

        public ActionResult ViewCarpoolJoined()
        {
            int idNum = 0;
            idNum = LoggedInUser.Id;

            if (idNum == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var viewJModel = new List<CarPoolDataSetToView>();

            ComuteDBEntities db = new ComuteDBEntities();
            CarpoolsJoined addUser = new CarpoolsJoined();

            int joinedCarPool = db.CarpoolsJoineds.Count();
            int theCarPools = db.CarpoolsTables.Count();
            int tempCID = 0;
            int OwnID;


            for (int i = 0; i < joinedCarPool; i++)
            {
                CarpoolsJoined carJoin = db.CarpoolsJoineds.FirstOrDefault(x => x.CarpoolJoinedID == i);
                if (carJoin == null)
                {
                    OwnID = 0;
                }
                else
                {
                    OwnID = carJoin.CarpoolJoinedID;
                }
                if (LoggedInUser.Id == OwnID)
                {
                    tempCID = carJoin.TheCarPoolID;
                    CarpoolsTable carpool = db.CarpoolsTables.FirstOrDefault(x => x.CarpoolID == tempCID);
                    int tempUID = carpool.OwnerID;
                    viewJModel.Add(new CarPoolDataSetToView
                    {
                        Name = db.UsersLists.ElementAt(tempUID).Name,
                        Origin = carpool.Origin,
                        Destination = carpool.Destination,
                        Days = carpool.DaysAvailable,
                        Seats = carpool.AvailableSeats,
                        DepartTime = carpool.DepartureTime,
                        ExpectTime = carpool.ExpectedTime,
                        Notes = carpool.Notes
                    });
                }
            }

            return View(viewJModel);
        }
    }
}