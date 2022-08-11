using AutoMapper;
using CoMute.Core.Domain;
using CoMute.Core.Interfaces.Repositories;
using CoMute.Web.Models.Dto;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _usersRepository;
        private readonly IMappingEngine _mappingEngine;

        public UserController(IUserRepository usersRepository, IMappingEngine mappingEngine)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mappingEngine = mappingEngine ?? throw new ArgumentNullException(nameof(mappingEngine));
        }

        [HttpGet]
        [Route("api/user/get")]
        public HttpResponseMessage Get()
        {
            var users = _usersRepository.GetAllUsers();
            if (users != null)
                return Request.CreateResponse(HttpStatusCode.Created, users);
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("api/user/getby")]
        public HttpResponseMessage Get(string id)
        {
            var user = _usersRepository.GetById(Guid.Parse(id));
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.Created, user);
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("api/user/add")]
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            var existingUser = _usersRepository.GetUserByEmail(registrationRequest.EmailAddress);
            if (existingUser != null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "User already exist!");

            var user = new User();
            CreatePassHash(registrationRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);
            if (ModelState.IsValid)
            {
                registrationRequest.UserId = Guid.NewGuid();
                user = _mappingEngine.Map<User>(registrationRequest);
                user.PasswordSalt = passwordSalt;
                user.PasswordHash = passwordHash;
                _usersRepository.Save(user);
            }
            return Request.CreateResponse(HttpStatusCode.Created, user);
        }

        [HttpPut]
        [Route("api/user/update")]
        public HttpResponseMessage Put(UserRequest userRequest)
        {
            var existingUser = _usersRepository.GetById(userRequest.UserId);
            if (existingUser == null || userRequest.UserId == Guid.Empty)
                return Request.CreateResponse(HttpStatusCode.NotFound, "User doesn't exist!");

            var user = new User();
            if (ModelState.IsValid)
            {            
                user = _mappingEngine.Map<User>(userRequest);
                user.PasswordHash = existingUser.PasswordHash;
                user.PasswordSalt = existingUser.PasswordSalt;
                _usersRepository.Update(user);
            }
            return Request.CreateResponse(HttpStatusCode.Created, user);
        }

        private void CreatePassHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            };
        }
    }
}
