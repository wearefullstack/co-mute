using CoMute.Web.Data;
using CoMute.Web.Data.DataAccess;
using CoMute.Web.Models;
using CoMute.Web.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class CarpoolController : Controller
    {
        // GET: Carpool
        private readonly RepWrapper repo;
        public int iPageSize = 5;

        public CarpoolController(RepWrapper _repo)
        {
            repo = _repo;
        }

        #region Carpool Interactions

        [HttpGet]
        public ActionResult Join(int id)
        {
            var carpool = repo.Carpools.GetById(id);
            if (carpool == null)
                return RedirectToAction("Index");
            else
            {
                return View(carpool);
            }
        }

        [HttpPost]
        public ActionResult Join(Carpool carpool)
        {
            if (carpool != null)
            {
                carpool = repo.Carpools.GetById(carpool.CarpoolID);
                if (carpool.iAvailableSeats > 0)
                {
                    repo.Carpools.Delete(carpool);
                    repo.Save();
                    
                    carpool.PassengerIds = AddToCSV(carpool.PassengerIds, User.Identity.GetUserId());
                    carpool.iAvailableSeats--;
                }

                repo.Carpools.Create(carpool);
                repo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Unable to join Carpool Route";
                return View(carpool);
            }
        }

        [HttpGet]
        public ActionResult Leave(int id)
        {
            var carpool = repo.Carpools.GetById(id);
            if (carpool == null)
                return RedirectToAction("Index");
            else
            {
                return View(carpool);
            }
        }

        [HttpPost]
        public ActionResult Leave(Carpool carpool)
        {
            if (carpool != null)
            {
                carpool = repo.Carpools.GetById(carpool.CarpoolID);

                repo.Carpools.Delete(carpool);
                repo.Save();
                string myID = User.Identity.GetUserId();
                carpool.PassengerIds = removeID(carpool.PassengerIds,myID);                   
                carpool.iAvailableSeats++;
                
                string test = carpool.PassengerIds;
                repo.Carpools.Create(carpool);
                repo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Unable to join Carpool Route";
                return View(carpool);
            }
        }

        #endregion Carpool InteractionsS

        #region CRUD

        #region Add
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Action = "Add";
            Carpool _carpool = new Carpool();
            _carpool.UserID = User.Identity.GetUserId();
            _carpool.UserName = User.Identity.GetUserName();
            return View("Edit", _carpool);
        }
     
        [HttpPost]
        public ActionResult Add(Carpool carpool)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (carpool.CarpoolID == 0)
                    {
                        repo.Carpools.Create(carpool);
                        //Message = $"Student {carpool.CarpoolID} added succesfully.";
                    }

                    repo.Save();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            //ViewBag.Action = (carpool.CarpoolID == 0) ? "Add" : "Edit";
            ViewBag.Action = "ADD";
            return View(carpool);
        }
        #endregion Add

        #region Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            return View(repo.Carpools.GetById(id));
        }
        //new
        [HttpPost]
        public ActionResult Edit(Carpool carpool)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (carpool.CarpoolID == 0)
                    {
                        repo.Carpools.Create(carpool);
                    }
                    else
                    {
                        Carpool carpool1 = repo.Carpools.GetById(carpool.CarpoolID);
                        repo.Carpools.Update(carpool, carpool1);
                    }

                    repo.Save();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            ViewBag.Action = (carpool.CarpoolID == 0) ? "Add" : "Edit";
            return View(carpool);
        }

        #endregion Edit

        #region Delete
        [HttpGet]
        public ActionResult DeleteCarpool(int id)
        {
            var carpool = repo.Carpools.GetById(id);
            if (carpool == null)
                return RedirectToAction("Index");
            else
            {
                return View(carpool);
            }
        }

        [HttpPost]
        public ActionResult DeleteCarpool(Carpool carpool)
        {
            if (carpool != null)
            {
                carpool = repo.Carpools.GetById(carpool.CarpoolID);
                repo.Carpools.Delete(carpool);
                repo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Unable to Delete student";
                return View(carpool);
            }
        }
        #endregion Delete
        
        #endregion CRUD

        #region Landing Page
        public ActionResult Index(string sortBy = "Owner", string searchString = "", int page = 1)
        {
            IEnumerable<Carpool> Carpools;
            Expression<Func<Carpool, Object>> _orderBy;
            string orderByDirection;
            int iTotalCarpoolRoutes;

            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = "Owner";
            }

            if (sortBy.EndsWith("_desc"))
            {
                sortBy = sortBy.Substring(0, sortBy.Length - 5);
                orderByDirection = "desc";
            }
            else
            {
                orderByDirection = "asc";
            }

            _orderBy = x => x.GetType().GetProperty(sortBy);
            iTotalCarpoolRoutes = repo.Carpools.FindAll().Count();
            if (searchString == "")
            {
                //iTotalCarpoolRoutes = repo.Carpools.FindAll().Count();
                //Carpools = repo.Carpools.GetWithOptions(new QueryOptions<Carpool>
                //{
                //    OrderBy = _orderBy,
                //    OrderByDirection = orderByDirection,
                //    Where = s => s.Owner.ToString().Contains(searchString),
                //    PageNumber = page,
                //    PageSize = iPageSize

                //});
            }
            else
            {
                iTotalCarpoolRoutes = repo.Carpools.FindByCondition(s => s.ToString().Contains(searchString) || s.UserName.ToString().Contains(searchString)).Count();
                Carpools = repo.Carpools.GetWithOptions(new QueryOptions<Carpool>
                {
                    OrderBy = _orderBy,
                    OrderByDirection = orderByDirection,
                    Where = s => s.UserName.Contains(searchString),
                    PageNumber = page,
                    PageSize = iPageSize
                });
            }

            return View(new CarpoolListViewModel
            {
                Carpools = repo.Carpools.FindAll(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = iPageSize,
                    TotalItems = iTotalCarpoolRoutes
                },
                myId = User.Identity.GetUserId()
            }) ;
            //return View(new CarpoolListViewModel
            //{
            //    Carpools = Carpools,
            //    PagingInfo = new PagingInfo
            //    {
            //        CurrentPage = page,
            //        ItemsPerPage = iPageSize,
            //        TotalItems = iTotalCarpoolRoutes
            //    }
            //});
        }

        #endregion Landing Page

        #region Private Helpers
        private string AddToCSV(string MainString, string stringToAdd)
        {
            if (String.IsNullOrEmpty(MainString))
            {
                MainString = stringToAdd;
            }
            else
            {
                MainString += "," + stringToAdd;
            }
            return MainString;
        }

        private List<string> getListIDs(string MainString)
        {
            return MainString.Split(',').ToList();
        }
        private string[] getArrayIDs(string MainString)
        {
            return MainString.Split(',').ToArray();
        }

        private string toCSV(List<string> lstStrings)
        { 
            return String.Join(",", lstStrings.Select(x => x.ToString()).ToArray());
        }

        private string removeID(string MainString, string idToRemove) 
        {
            int iStart = MainString.IndexOf(idToRemove);
            int iLen = idToRemove.Length;
            string sTemp = MainString.Substring(iStart, iLen);

            if (iStart == 0)
            {
                if (MainString.IndexOf(',') == -1) 
                { MainString = ""; }
                else
                { MainString = sTemp.Replace(idToRemove + ",", ""); }
                
            }
            else
            {
                MainString = sTemp.Replace("," + idToRemove, "");
            }

            return MainString;
            
        }
        #endregion Private Helpers
    }
}