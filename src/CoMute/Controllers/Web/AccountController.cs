using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using AllowAnonymousAttribute = System.Web.Mvc.AllowAnonymousAttribute;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (TempData.ContainsKey(Constants.LOGIN_REQUEST))
            {
                LoginRequest loginRequest = (LoginRequest)TempData[Constants.LOGIN_REQUEST];
                HttpResponseMessage response = await ComuteService.LoginUser(loginRequest);
                if (response.IsSuccessStatusCode)
                {
                    UserDto userDto = await response.Content.ReadAsAsync<UserDto>();
                    Session[Constants.PROFILE_DATA] = userDto;
                    FormsAuthentication.SetAuthCookie(userDto.Id.ToString(), false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", response.ReasonPhrase);
                    return View(loginRequest);
                }
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await ComuteService.LoginUser(loginRequest);
                if (response.IsSuccessStatusCode)
                {
                    UserDto userDto = await response.Content.ReadAsAsync<UserDto>();
                    Session[Constants.PROFILE_DATA] = userDto;
                    FormsAuthentication.SetAuthCookie(userDto.Id.ToString(), false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                    return View(loginRequest);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(loginRequest);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await ComuteService.RegisterUser(registrationRequest);
                if (response.IsSuccessStatusCode)
                {
                    LoginRequest loginRequest = await response.Content.ReadAsAsync<LoginRequest>();
                    TempData[Constants.LOGIN_REQUEST] = loginRequest;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", response.ReasonPhrase);
                    return View(registrationRequest);
                }
            }
            return View(registrationRequest);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}