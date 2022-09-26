using CoMute.Web.Data;
using CoMute.Web.Models.Dto;
using Hanssens.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Controllers.Web
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult AddPool()
        {
            return View();
        }

    
        public ActionResult SendEdit()
        {

            RegistrationRequest student = null;

           // var id = Convert.ToInt32(TempData["ID"]);
            var id = (int)System.Web.HttpContext.Current.Session["ID"];

            using (var client = new HttpClient())
            {
             
                var userid = TempData["ID"];
                client.BaseAddress = new Uri("http://localhost:59598/api/UpdateUser");




                //HTTP GET
                /*  var responseTask = client.GetAsync("UpdateUser" +student);*/
                var responseTask = client.GetAsync("UpdateUser?id=" + id.ToString());
                responseTask.Wait();
                if ( id==0)
                {
                    return RedirectToAction("Index");
                }

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RegistrationRequest>();
                    readTask.Wait();

                    student = readTask.Result;
                }
            }

            return View(student);
        }


        //View Data on Edit
        public ActionResult Edit()
        {


            /*var id = Convert.ToInt32(TempData["ID"]);*/
            if(Session["ID"] is int)
            {
                var id = (int)System.Web.HttpContext.Current.Session["ID"];

                var student = db.Registers.Select(zz => new RegistrationRequest
                {
                    RegisterID = zz.Register_ID,
                    EmailAddress = zz.Email,
                    PhoneNumber = zz.Phone.ToString(),
                    Surname = zz.Surname,
                    Name = zz.Name
                }).Where(x => x.RegisterID == id).FirstOrDefault();

                using (var client = new HttpClient())
                {

                    var userid = TempData["ID"];
                    client.BaseAddress = new Uri("http://localhost:59598/api/UpdateUser");




                    //HTTP GET
                    /*   var responseTask = client.GetAsync("UpdateUser" + user);*/
                    var responseTask = client.GetAsync("UpdateUser?id=" + id.ToString());
                    responseTask.Wait();
                    /*   if (id == 0)
                       {
                           return RedirectToAction("Index");
                       }*/

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<RegistrationRequest>();
                        readTask.Wait();

                        student = readTask.Result;
                    }
                }

                return View(student);
            }
            else
            {
                return RedirectToAction("Index");
            }

           
        }



        //Send Updated Data of the User
        [HttpPost]
        public ActionResult Edit(RegistrationRequest student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/UpdateUser");

                //HTTP POST
                var putTask = client.PutAsJsonAsync("UpdateUser", student);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["MsgChangeStatus"] = "You have successfully updated your profile information!";

                  //  return RedirectToAction("Index");
                }
            }
            return View(student);
        }


        //Register User

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ChangeStatus()
        {
            return View();
        }


        //Login
        CarPoolEntities db = new CarPoolEntities();
        [HttpPost]
        public ActionResult Index(LoginRequest loginRequest)
        {
            var user = db.Registers.Where(zz => zz.Email == loginRequest.Email && zz.Password == loginRequest.Password).FirstOrDefault();
            /* ViewBag.UserId = user.Register_ID;*/
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Credetials");
                return View();
            }
            else
            {
                TempData["ID"] = user.Register_ID;
                Session["ID"] = user.Register_ID;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59598/api/Authentication");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync("Authentication", loginRequest);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Search");
                    }
                }
                return View(loginRequest);

            }
               // ModelState.AddModelError(string.Empty, "Invalid Credetials");

             //   return View(loginRequest);
            
        }

        //Send Register Data
        [HttpPost]
        public ActionResult Register(RegistrationRequest student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/register");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("register", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }



            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(student);
        }

        //Add Pool

        [HttpPost]
        public ActionResult AddPool(CarPool pool)
        {

            if (Session["ID"] is int)
            {
                var id = (int)System.Web.HttpContext.Current.Session["ID"];
                //////////////Check Existing User Pools


                List<CarPool> carpool = null;

                using (var clients = new HttpClient())
                {
                    clients.BaseAddress = new Uri("http://localhost:59598/api/");
                    //HTTP GET
                    var responseTasks = clients.GetAsync("ExistingUserCarPool/" + id);
                    responseTasks.Wait();

                    var results = responseTasks.Result;
                    if (results.IsSuccessStatusCode)
                    {
                        var readTasks = results.Content.ReadAsAsync<List<CarPool>>();
                        readTasks.Wait();

                        carpool = readTasks.Result;

                        var counter = 0;

                        foreach (var car in carpool)
                        {
                            if ((pool.Departure >= car.Departure && pool.Departure < car.Expected_Arrival) || (pool.Expected_Arrival <= car.Expected_Arrival && pool.Expected_Arrival > car.Departure))
                            {
                                Console.WriteLine("ERRRRRR");
                                counter++;
                              //  ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                            }
                            else
                            {
                                Console.WriteLine("Good");
                               // ModelState.AddModelError(string.Empty, "Good");
                            }
                        }
                        if (counter > 0)
                        {
                         //   TempData["MsgChangeStatus"] = "You cannot create this pool opportunity as its timeframe overlap with some of the car pool/s you have created!";
                           // return RedirectToAction("AddPool");

                        }
                        else if (counter == 0)
                        {

                            using (var client = new HttpClient())
                            {

                                pool.Register_ID = id;
                                client.BaseAddress = new Uri("http://localhost:59598/api/addPool");

                                //HTTP POST
                                var postTask = client.PostAsJsonAsync("addPool", pool);
                                postTask.Wait();

                                var result = postTask.Result;
                                if (result.IsSuccessStatusCode)
                                {
                                    return RedirectToAction("Search");
                                }
                            }


                        }

                    }
                    else //web api sent error response 
                    {
                        //log response status here..



                        ModelState.AddModelError(string.Empty, "Please Select a Departure time and arrival time that does not overlap with your existing pools.");
                    }
                }




                ///////////////////




                ModelState.AddModelError(string.Empty, "Please Select a Departure time and arrival time that does not overlap with your existing pools.");
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(pool);
            

           
        }




        //View Pool Opportunities
        [HttpGet]
        public ActionResult Search()
        {
           List<CarPool> carpool = null;

         // var id = (int)System.Web.HttpContext.Current.Session["ID"];

            if (Session["ID"] is int)
            {

                ViewBag.id = (int)System.Web.HttpContext.Current.Session["ID"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59598/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("carpool");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<CarPool>>();
                        readTask.Wait();

                        carpool = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..



                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }

                    return View(carpool);
                }
             //   return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

           
        }


       //Join Car Pool Opportunity
        [HttpPost]
        public ActionResult Join(int id )
        {
           // int userid = Convert.ToInt32(TempData["ID"]);
            var userid = (int)System.Web.HttpContext.Current.Session["ID"];

            Passenger_Pool student = new Passenger_Pool();
            student.Register_ID = userid;
            student.User_Car_Pool_ID = id;

            CarPool curr = null;
            ////Get Car Pool By Id
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/");
                //HTTP GET
                var responseTask = client.GetAsync("poolTimes/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CarPool>();
                    readTask.Wait();

                    curr = readTask.Result;


                    ///////////////////////////////Joined by userr
                    ///
                    List<CarPool> carpool = null;

                    using (var clients = new HttpClient())
                    {
                        clients.BaseAddress = new Uri("http://localhost:59598/api/");
                        //HTTP GET
                        var responseTasks = clients.GetAsync("BookedCarPool/" + userid);
                        responseTask.Wait();

                        var results = responseTasks.Result;
                        if (results.IsSuccessStatusCode)
                        {
                            var readTasks = results.Content.ReadAsAsync<List<CarPool>>();
                            readTask.Wait();

                            carpool = readTasks.Result;

                            var counter=0;

                            foreach(var car in carpool)
                            {
                                if((curr.Departure >= car.Departure && curr.Departure<car.Expected_Arrival ) ||( curr.Expected_Arrival<=car.Expected_Arrival && curr.Expected_Arrival>car.Departure))
                                {
                                    Console.WriteLine("ERRRRRR");
                                    counter++;
                                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                                }
                                else
                                {
                                    Console.WriteLine("Good");
                                    ModelState.AddModelError(string.Empty, "Good");
                                }
                            }
                            if(counter > 0)
                            {
                                TempData["MsgChangeStatus"] = "You cannot join this pool opportunity as its timeframe overlap with some of the car pool/s you have joined!";
                                return RedirectToAction("Search");
                               
                            }
                            else if (counter == 0)
                            {
                               

                                /// Book

                               

                                using (var client1 = new HttpClient())
                                {
                                    client1.BaseAddress = new Uri("http://localhost:59598/api/BookPool");

                                    //HTTP POST
                                    var postTask1 = client1.PostAsJsonAsync("BookPool", student);
                                    postTask1.Wait();

                                    var result1 = postTask1.Result;
                                    /* if (result.IsSuccessStatusCode)
                                     {
                                         return RedirectToAction("EditPassengers", new { param1 = id });
                                     }*/
                                }



                 
                                // Update Number Of Passengers
                                CarPool pool = new CarPool(); ;
                                pool.User_Car_Pool_ID = id;
                                using (var client2 = new HttpClient())
                                {

                                    client2.BaseAddress = new Uri("http://localhost:59598/api/updateCarPool");

                                  
                                    var putTask2 = client2.PutAsJsonAsync("updateCarPool", pool);
                                    putTask2.Wait();

                                    var result2 = putTask2.Result;
                                    if (result2.IsSuccessStatusCode)
                                    {

                                        return RedirectToAction("BookedPool");
                                    }
                                }

                            }

                        }
                        else //web api sent error response 
                        {
                            //log response status here..



                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }

                    ////////////////////////////
                }
                else //web api sent error response 
                {
                    //log response status here..



                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }


           



            //////////////////////////////////


           


         
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(student);
        }

       

        ///Booked Car Pool
        ///
        [HttpGet]
        public ActionResult BookedPool()
        {

            // int userid = Convert.ToInt32(TempData["ID"]);

            if (Session["ID"] is int)
            {
                var userid = (int)System.Web.HttpContext.Current.Session["ID"];

                List<CarPool> carpool = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59598/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("BookedCarPool/" + userid);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<CarPool>>();
                        readTask.Wait();

                        carpool = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..



                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(carpool);
            }

            else
            {
                return RedirectToAction("Index");
            }
        }




        ///
         //Send Cancel Car Pool
        [HttpPost]
        public ActionResult Cancel(int id , int PoolId)
        {

            var usercarpoolId = PoolId;


            PassengerCarPool pool =new PassengerCarPool();
            pool.Passenger_Pool_ID=id;

            CarPool userpool = new CarPool();
            userpool.User_Car_Pool_ID=usercarpoolId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/updatePassngers");

                //HTTP POST
                var putTask = client.PutAsJsonAsync("updatePassngers", userpool);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                
                }
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59598/api/CancelPool");

                //HTTP POST
                var putTask = client.PutAsJsonAsync("CancelPool", pool);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["MsgChangeStatus"] = "You have successfully left the car-pool opportunity!";
                    return RedirectToAction("BookedPool");
                }
            }


            return View(pool);
        }

    }
}