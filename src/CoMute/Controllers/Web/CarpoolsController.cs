using CoMute.Web.Data;
using CoMute.Web.Models.Dto;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Razor.Parser;

namespace CoMute.Web.Controllers.API
{
    public class CarpoolsController : Controller
    {

        dbCoMuteEntities db = new dbCoMuteEntities();


        /// <summary>
        /// Http response to display view: user's car pools
        /// </summary>
        /// <returns></returns>
       // [Route("carpool")]
        public ActionResult Index()
        {
            return View(db.tblUserCarPools.ToList());
        }


        public ActionResult CreateCarpool()
        {
            return View();
        }

        
        /// <summary>
        /// Method to add a carpool to the database
        /// </summary>
        /// <param name="carpoolToCreate"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult CreateCarpool([Bind(Exclude = "CarPoolID")] tblUserCarPool carpoolToCreate)
        {
            var userID = (int)System.Web.HttpContext.Current.Session["ID"];
            string ownerName = GetOwner((int)userID);

            if (userID is int)
            {
                if (ModelState.IsValid)
                {
                    //Start new Http session       
                    using (var client = new HttpClient())
                    {
                        //Use GET response to check if carpool already exists based on userID
                        client.BaseAddress = new Uri("http://localhost:59598/api/");
                        var task = client.GetAsync("GetCarpools/" + userID.ToString());

                        task.Wait();

                        var result = task.Result;

                        //Check if the response was successful
                        if (result.IsSuccessStatusCode)
                        {
                            //Some local variables
                            List<Carpool> carpool = new List<Carpool>();
                            bool bFlag = false;

                            //Read Carpool object
                            var readTask = result.Content.ReadAsAsync<List<Carpool>>();
                            readTask.Wait();
                            carpool = readTask.Result;

                            using (var client2 = new HttpClient())
                            {
                                carpoolToCreate.UserID = Int32.Parse(userID.ToString());
                                carpoolToCreate.Owner_Leader = ownerName;
                                carpoolToCreate.PassengerPoolID = "PP" + userID.ToString();
                                client2.BaseAddress = new Uri("http://localhost:59598/api/CreateCarpool");

                                //New POST request
                                var task2 = client.PostAsJsonAsync("CreateCarpool", carpoolToCreate);
                                task2.Wait();

                                var result2 = task2.Result;

                                if (result2.IsSuccessStatusCode)
                                {
                                    return RedirectToAction("Index", "Carpools");
                                }

                            }
                        }
                        else
                        {
                            //If task was not successful
                            return RedirectToAction("Index", "Carpools");
                        }

                    }
                }

            }

            return View(carpoolToCreate);
        }


        #region private helpers
        /// <summary>
        /// Method to return current user's Full name
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        private string GetOwner(int userID)
        {
            string name = db.tblRegisters.Where(x => x.UserID == userID).Select(u => u.Name).Single();
            string surname = db.tblRegisters.Where(x => x.UserID == userID).Select(u => u.Surname).Single();

            return name + " " + surname;

        }
        #endregion

    }
}