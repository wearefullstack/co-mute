using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoMute.Web.Models;

namespace CoMute.Web.Controllers
{
    public class UserController : Controller
    {
        regmvcEntities4 regmvcEntities = new regmvcEntities4();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            int user = Convert.ToInt32(Session["UseId"]);
            if (user == 0)
            {
                return RedirectToAction("Login", "User");
            }
            return View(regmvcEntities.userregs.Find(user));
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(userreg userreg)
        {
            regmvcEntities.userregs.Add(userreg);
            regmvcEntities.SaveChanges();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(userreg userreg)
        {
            bool exists = regmvcEntities.userregs.Any(u => u.Email == userreg.Email && u.Password == userreg.Password);
            if (exists)
            {
                Session["UseId"] = regmvcEntities.userregs.Single(x => x.Email == userreg.Email).Id;              
                return RedirectToAction("GetCarJoined", "Car");
            }
            ViewBag.Message = "Invalid";
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["UseId"] = 0;
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult UserEdit(int id)
        {
            var obj = regmvcEntities.userregs.SingleOrDefault();
            return View(obj);
        }

        [HttpPost]
        public ActionResult UserEdit(userreg userreg)
        {
            regmvcEntities.Entry(userreg).State = System.Data.Entity.EntityState.Modified;
            regmvcEntities.SaveChanges();
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
