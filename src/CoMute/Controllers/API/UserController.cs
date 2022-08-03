using AutoMapper;
using CoMute.Core.Domain;
using CoMute.Core.Interfaces.Repositories;
using CoMute.DB.FakeRepository;
using CoMute.Web.Models.Dto;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{    
    public class UserController : ApiController
    {
        private readonly IUserRepository _usersRepository;
        private readonly IMappingEngine _mappingEngine;
        private FakeUsersRepository userRepo = new FakeUsersRepository();
        public UserController(IUserRepository usersRepository, IMappingEngine mappingEngine)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mappingEngine = mappingEngine ?? throw new ArgumentNullException(nameof(mappingEngine));
        }

        [Route("api/user/add")]
        [HttpPost]
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            var user = new User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress
            };

            userRepo.Save(user);                      

            return Request.CreateResponse(HttpStatusCode.Created, user); ;
        }
    }
}
