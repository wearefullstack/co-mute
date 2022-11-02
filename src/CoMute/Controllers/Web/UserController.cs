using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CoMute.Web.Service;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace CoMute.Web.Controllers.Web
{
    [Authorize]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Account()
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            HttpResponseMessage responseMessage = await ComuteService.GetUser(user.Id);
            if (responseMessage.IsSuccessStatusCode)
            {
                EditDto userDto = await responseMessage.Content.ReadAsAsync<EditDto>();
                return View(userDto);
            }
            return View("Error");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Account([FromBody] EditDto editDto)
        {
            UserDto user = Session[Constants.PROFILE_DATA] as UserDto;
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }
            editDto.Id = user.Id;
            if (ModelState.IsValid)
            {
                HttpResponseMessage httpResponseMessage = await ComuteService.EditUser(editDto);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    TempData["Status"] = "Updated";
                    return RedirectToAction("Account");
                }
            }
            return View(editDto);
        }

    }
}