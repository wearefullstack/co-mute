using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.Models;

namespace CoMute.Web.Controllers
{
    public class CarController : Controller
    {
        regmvcEntities2 regmvcEntities1 = new regmvcEntities2();
        // GET: Car
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CarRegister()
        {
            //return View();
            int user = Convert.ToInt32(Session["UseId"]);
            if (user == 0)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View();
            }
            //return user.Equals(null) ? RedirectToAction("Login", "User") : (ActionResult)RedirectToAction("CarRegister", "Car");
        }

        [HttpPost]
        public ActionResult CarRegister(carreg carreg)
        {
            //Todo:
            //Statement to compare values enetered in the Departure_Time and Expected_Arrival_Time text fields,
            //to existing Departure_Time and Expected_Arrival_Time values in the carreg table. If the time-frames overlap, the Owner would be notified
            //and they will have to enter time-frames in the text fields that do not overlap with existing time-frames in the carreg table.
            regmvcEntities1.carregs.Add(carreg);
            regmvcEntities1.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult GetCar(userreg userreg)
        { 
            //Todo:
            //Statement to display Car-pool Opportunities WHERE Owner/Leader FROM carreg table equals Name FROM userreg table
            //This will only display Opportunities where the logged in account is the Owner of a Car-pool Opportunity
            var obj = regmvcEntities1.carregs.ToList();
            return View(obj);
        }

        [HttpGet]
        public ActionResult GetCarJoined()
        {
            //Todo:
            //Statement to return all Car-pool Opportunities where the logged in account has joined
            var obj = regmvcEntities1.carregs.ToList();
            return View(obj);
        }

        [HttpGet]
        public ActionResult JoinLeave()
        {
            var obj = regmvcEntities1.carregs.ToList();
            return View(obj);
        }

        [HttpPost]
        public ActionResult JoinLeave(carreg carreg)
        {
            //Todo:
            //Modify to join logged in user under selected Car-pool Opportunity
            regmvcEntities1.Entry(carreg).State = System.Data.Entity.EntityState.Modified;
            regmvcEntities1.SaveChanges();
            return View();
        }

        // GET: Car/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Car/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
