using CoMute.Web.Data;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        private readonly ComuteContext _comuteContext;

        public UserController()
        {
            _comuteContext = new ComuteContext();
        }

        [Route("api/user/add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post(RegistrationRequest registrationRequest)
        {
            User user = new User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress,
                PhoneNumber = registrationRequest.PhoneNumber,
                Password = registrationRequest.Password
            };

            _comuteContext.Users.Add(user);
            await _comuteContext.SaveChangesAsync();
            LoginRequest loginRequest = new LoginRequest()
            {
                Email = user.EmailAddress,
                Password = user.Password
            };
            return Request.CreateResponse(HttpStatusCode.Created, loginRequest);
        }

        [Route("api/user/{userId}")]
        [HttpGet]
        public HttpResponseMessage Get(int userId)
        {
            User user = _comuteContext.Users.Find(userId);
            if(user != null)
            {
                EditDto editDto = new EditDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    EmailAddress = user.EmailAddress,
                    PhoneNumber = user.PhoneNumber
                };
                return Request.CreateResponse(HttpStatusCode.OK, editDto);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("api/user/update")]
        [HttpPost]
        public HttpResponseMessage Update(EditDto editDto)
        {
            User user = _comuteContext.Users.SingleOrDefault(x => x.Id == editDto.Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(editDto.Name))
                {
                    user.Name = editDto.Name;
                }
                if (!string.IsNullOrEmpty(editDto.Surname))
                {
                    user.Surname = editDto.Surname;
                }
                if (!string.IsNullOrEmpty(editDto.EmailAddress))
                {
                    user.EmailAddress = editDto.EmailAddress;
                }

                user.PhoneNumber = editDto.PhoneNumber;

                if (!string.IsNullOrEmpty(editDto.Password) && !string.IsNullOrEmpty(editDto.ConfirmPassword))
                {
                    user.Password = editDto.Password;
                }

                _comuteContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
