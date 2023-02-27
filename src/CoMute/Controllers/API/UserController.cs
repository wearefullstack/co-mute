using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using CoMute.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;


namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        private UserRepository _UserRepository = new UserRepository();

        [Route("api/user")]
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            //Guid uid = new Guid();
            var user = new User()
            {
                UserGuid = Guid.NewGuid(),
                FirstName = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                Email = registrationRequest.EmailAddress,
                Phone = registrationRequest.PhoneNumber,
                Password = registrationRequest.Password,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            _UserRepository.AddUser(user);

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }
        [Route("api/Authentication")]
        public AuthenticateResponse SignIn(LoginRequest loginRequest)
        {

            AuthenticateResponse authenticateResponse= _UserRepository.Sign(loginRequest);
            if (authenticateResponse !=null)
            {

            }
            else
            {

            }

            return authenticateResponse;
        }

        //Get 
        [Route("api/profile/{userId}")]
        public AuthenticateResponse GetUserProfile(string userId)
        {
            
            AuthenticateResponse authenticateResponse = _UserRepository.GetUser(userId);

            return authenticateResponse;
        }

        [Route("api/UpdateUser")]
        public AddingResponse UpdateUserProfile(RegistrationRequest registrationRequest)
        {
            var todayDate = DateTime.Now;
            var user = new User()
            {
                FirstName = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                Phone = registrationRequest.PhoneNumber,
                Email = registrationRequest.EmailAddress,
                Password = registrationRequest.Password,
                ModifiedDate = todayDate,
                UserGuid = registrationRequest.UserGuid
            };

            AddingResponse response = new AddingResponse();
            bool  result = _UserRepository.UpdateUser(user);
            string updateResponse = "UnSuccessful";

            if (result)
            {
                updateResponse = "Success";

                response.response = updateResponse;
            }

            
            return response;
        }

    }
}
