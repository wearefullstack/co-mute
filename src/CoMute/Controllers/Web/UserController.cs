using CoMute.Web.Controllers.Web.Helpers;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CoMute.Web.Controllers.Web
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Profile()
        {
            var userDetails = new UserRequest();
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = GetUsers(client);
                if (result.IsSuccessStatusCode)
                {
                    Task<List<UserRequest>> readTask = Process(result);
                    var identity = (ClaimsIdentity)User.Identity;

                    if (!string.IsNullOrEmpty(identity.Name))
                        userDetails = readTask.Result.FirstOrDefault(x => x.UserId == Guid.Parse(identity.Name));
                    else
                        FormsAuthentication.RedirectToLoginPage();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(userDetails);
        }
        
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var userDetails = new UserRequest();
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = GetUsers(client);
                if (result.IsSuccessStatusCode)
                {
                    Task<List<UserRequest>> readTask = Process(result);
                    userDetails = readTask.Result.FirstOrDefault(x => x.UserId == id);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(userDetails);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserRequest userRequest)
        {      
            await new HttpHelper<UserRequest>()
                .PutRestServiceDataAsync("user/update", userRequest);
            
            return RedirectToAction("Profile");
        }


        private static Task<List<UserRequest>> Process(HttpResponseMessage result)
        {
            var readTask = result.Content.ReadAsAsync<List<UserRequest>>();
            readTask.Wait();
            return readTask;
        }

        private static HttpResponseMessage GetUsers(HttpClient client)
        {
            client.BaseAddress = new Uri("http://localhost:59598/api/");
            //HTTP GET
            var responseTask = client.GetAsync("user/get");
            responseTask.Wait();

            var result = responseTask.Result;
            return result;
        }


    }
}