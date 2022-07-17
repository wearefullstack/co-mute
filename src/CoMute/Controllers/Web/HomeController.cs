using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        private static string currentUser { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Users user)
        {
            using (CoMuteEntities1 db = new CoMuteEntities1())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var hashedPassword = MD5HashPasswords(user.Password);
                        var _password = db.Users.Where(x => x.Password == hashedPassword).FirstOrDefault();
                        var _email = db.Users.Where(y => y.Email == user.Email).FirstOrDefault();
                        if (_password != null && _email != null)
                        {
                            
                            currentUser = user.Email;
                            ViewBag.Message = "Successful Login!";
                            return RedirectToAction("ViewPoolJoined");
                        }
                        else
                        {
                            ViewBag.Message = "Login failed! Please try again!";
                            return View("Index");
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error : " + e.Message;
                    throw;
                }
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users model)
        {
            using (CoMuteEntities1 db = new CoMuteEntities1())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var checkEmailRegistered = db.Users.Where(e => e.Email == model.Email).FirstOrDefault();
                        if (checkEmailRegistered == null)
                        {
                            if (model.Password == model.ConfirmPassword)
                            {
                                Users user = new Users();
                                user.Name = model.Name;
                                user.Surname = model.Surname;
                                user.Phone = model.Phone;
                                user.Email = model.Email;
                                user.Password = MD5HashPasswords(model.Password);
                                user.ConfirmPassword = MD5HashPasswords(model.ConfirmPassword);
                                db.Users.Add(user);
                                db.SaveChanges();
                                currentUser = user.Email;
                                return RedirectToAction("RegisterPool");
                            }
                            else
                            {
                                ViewBag.Message = "Passwords do not match!";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "User Already Exists";
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error : " + e.Message;
                    throw;
                }
            }
            return View();
        }

        public ActionResult RegisterPool()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterPool(CarPools carPools)
        {
            using (CoMuteEntities1 db = new CoMuteEntities1())
            {
                try
                {
                    if (currentUser != "")
                    {
                        if (ModelState.IsValid)
                        {
                            var timeFrames = db.CarPools.Where(t => t.DepartureTime == carPools.DepartureTime).FirstOrDefault();

                            if (timeFrames == null && timeFrames.ToString() != carPools.DepartureTime.ToString())
                            {
                                CarPools pools = new CarPools();
                                pools.DepartureTime = carPools.DepartureTime;
                                pools.ExpectA_Time = carPools.ExpectA_Time;
                                pools.Origin = carPools.Origin;
                                pools.DaysAvailable = carPools.DaysAvailable;
                                pools.Destination = carPools.Destination;
                                pools.Avail_Seats = carPools.Avail_Seats;
                                pools.Owner_Leader = carPools.Owner_Leader;
                                pools.Notes = carPools.Notes;
                                pools.Email = currentUser;
                                pools.PoolCreationDate = DateTime.Now;
                                db.CarPools.Add(pools);
                                db.SaveChanges();
                                ViewBag.Message = "Car Pool Successfully Created!";
                                return RedirectToAction("ViewPoolCreated");
                            }
                            else
                            {
                                ViewBag.Message = "Overlapping Time Frames For Departure.";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Message = "User not logged in!";
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error : " + e.Message;
                    throw;
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult JoinCarPool(int? id)
        {
            //User gets to see car pool details and hit a button to confirm if they want to join car pool
            //decrement its then done to the available seats 
            return View();
        }

        [HttpPost]
        public ActionResult LeaveCarPool(int? id)
        {
            using (CoMuteEntities1 db = new CoMuteEntities1())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        //User gets to see car pool details and hit a button to confirm if they want to join car pool
                        //increment its then done to the available seats 
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error : " + e.Message;
                    throw;
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Search(CarPools carPools)
        {
            using (CoMuteEntities1 db = new CoMuteEntities1())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Users checkEmail = db.Users.Find(currentUser);
                        if (currentUser != " " && checkEmail != null)
                        {
                            return View(db.CarPools.ToList());
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error : " + e.Message;
                    throw;
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ViewPoolCreated()
        {
            using (CoMuteEntities1 db = new CoMuteEntities1())
            {
                try
                {
                    if (currentUser != " ")
                    {
                        return View(db.CarPools.ToList());
                    }

                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error : " + e.Message;
                    throw;
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ViewPoolJoined(JoinedCarPools joinedPools)
        {
            using (CoMuteEntities1 db = new CoMuteEntities1())
            {
                try
                {
                    var confirmEmail = db.JoinedCarPools.Where(e => e.JoinerEmail == currentUser).FirstOrDefault();
                    if (currentUser != " ")
                    {
                        ViewBag.CurrentUser = confirmEmail;
                        var poolList = db.JoinedCarPools.Where(u => u.JoinerEmail == currentUser).ToList();
                        return View(poolList);
                    }
                    else
                    {
                        ViewBag.Message = "No data!";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error : " + e.Message;
                    throw;
                }
            } 
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Profile(Users users)
        {
            using (CoMuteEntities1 db = new CoMuteEntities1() )
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Users updateUser = db.Users.Where(e => e.Email == currentUser).FirstOrDefault();
                        if (currentUser != " " && updateUser != null)
                        {
                            //var updateUser = new Users();
                            updateUser.Email = users.Email;
                            updateUser.Name = users.Name;
                            updateUser.Password = users.Password;
                            updateUser.Surname = users.Surname;
                            updateUser.Phone = users.Phone;
                            updateUser.ConfirmPassword = users.ConfirmPassword;
                             db.Entry(updateUser).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            if (ModelState.IsValid)
                            {
                                ViewBag.Message = "Asset record successfully inserted.";
                                ModelState.Clear();
                                ViewBag.Message = "Asset record successfully updated.";
                                return View("ViewPoolJoined");
                            }
                        }
                        else
                        {
                            return View("Profile");
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Error : " + e.Message;
                    throw;
                }
            }
            return View();
        }

        static public string MD5HashPasswords(string password)
        {
            using (var md5Hash = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = md5Hash.ComputeHash(sourceBytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hash;
            }
        }

    }
}