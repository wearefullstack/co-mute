using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoMute.Lib.Dto;
using CoMute.Lib.services;

namespace CoMute.Web.Controllers.API
{
    //public class OwnerController : ApiController
    //{
    //    private IUserService GetService()
    //    {
    //        return new UserService();
    //    }

    //    [Route("pool/owners")]
    //    public IList<UserDto> Get()
    //    {
    //        return GetService().GetOwners();
    //    }
    //}

    //[RoutePrefix("api/pools")]
    public class PoolController : ApiController
    {
        private IUserService GetService()
        {
            return new UserService();
        }

        public IEnumerable<PoolDto> Get(int id)
        {
            return GetService().GetOwnedPools(id);
        }

        //[HttpGet]
        //[Route("pool/owners")]
        public IEnumerable<UserDto> GetOwners()
        {
            return GetService().GetOwners();
        }

        [HttpPost]
        public HttpResponseMessage CreatePool([FromBody] PoolDto dto)
        {
            var result = GetService().CreatePool(dto);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpPost]
        public HttpResponseMessage JoinPool(int userId, int poolId)
        {
            var result = GetService().JoinPool(userId, poolId);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpPost]
        public HttpResponseMessage LeavePool(int userId, int poolId)
        {
            var result = GetService().LeavePool(userId, poolId);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

    }

    public class UserController : ApiController
    {
        //private readonly IUserService _service;

        //public UserController(IUserService service)
        //{
        //    _service = service;
        //}

        private IUserService GetService()
        {
            return new UserService();
        }


        [HttpPost]
        public HttpResponseMessage AddUser([FromBody] UserDto dto)
        {
            var result = GetService().AddUser(dto);

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [Route("user/add")]
        [HttpPost]
        public HttpResponseMessage Post(RegistrationRequest rr)
        {
            var dto = new UserDto
            {
                Email = rr.EmailAddress,
                Name = rr.Name,
                Password = rr.Password,
                Phone = rr.PhoneNumber,
                RegisterTime = DateTime.Now,
                Surname = rr.Surname,
                UserId = 0
            };

            var user = GetService().AddUser(dto);

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }


        public /*HttpResponseMessage*/ UserDto GetLogin([FromBody]LoginRequest request)
        {
            var result = GetService().GetLogin(request.Email, request.Password);
            return result;
            //return Request.CreateResponse(HttpStatusCode.Found, result);
        }

        public HttpResponseMessage GetJoinedPools(int userId)
        {
            var result = GetService().GetJoinedPools(userId);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        public HttpResponseMessage GetOwnedPools(int userId)
        {
            var result = GetService().GetOwnedPools(userId);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        //public HttpResponseMessage GetOtherPools(int userId)
        //{
        //    var result = GetService().GetOtherPools(userId);
        //    return Request.CreateResponse(HttpStatusCode.Created, result);
        //}

        public HttpResponseMessage GetOwners()
        {
            var result = GetService().GetOwners();
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        public HttpResponseMessage GetUserPageModel(int userId)
        {
            var result = GetService().GetUserPageModel(userId);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpPut]
        public HttpResponseMessage UpdateProfile(int userId, [FromBody] UserDto dto)
        {
            var result = GetService().UpdateProfile(userId, dto);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }
    }
}
