using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        private FullStackDBEntities db = new FullStackDBEntities();
        [Route("user/add")]
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            var user = new User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                Email = registrationRequest.EmailAddress,
                //EmailAddress = registrationRequest.EmailAddress

            };

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }

        [Route("api/User/getUsers")]
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }


        [Route("api/User/registerNewUser")]
        public HttpResponseMessage RegisterNewUser([FromBody] User user)
        {
            //bool uExist = UserExists(user.UserName);
            db.Configuration.ProxyCreationEnabled = false;

            User newUser = new User();
            //var hash = GenerateHash(user.UserPassword);
            newUser.Name = user.Name;
            newUser.Surname = user.Surname;
            newUser.Phone = user.Phone;
            newUser.Email = user.Email;
            newUser.Password = user.Password;            

            //if (uExist == false)
            //{
                try
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                var response = Request.CreateResponse(HttpStatusCode.OK, newUser);
                return response;
            //}
            //else
            //{
            //    var response = Request.CreateResponse(HttpStatusCode.BadRequest, "user Exists");
            //    return response;
            //}
        }

        //Customer Login
        [Route("api/User/userLogin")]
        [HttpPost]
        public dynamic CustomerLogin([FromBody] User usr)
        {
            //check if user exists
            User checkUserExist = db.Users.Where(usrw => usrw.Email == usr.Email).FirstOrDefault();
            if (checkUserExist == null)
            {
                dynamic retEmptyUser = new ExpandoObject();
                retEmptyUser.Message = "Invalid User!";
                return retEmptyUser;
            }

         
            User usrr = db.Users.Where(usrw => usrw.Email == usr.Email && usrw.Password == usr.Password)
                             .FirstOrDefault();
            if (usrr != null)
            {
                
                dynamic user = new ExpandoObject();
                user.User_ID = usrr.User_ID;
                user.Name = usrr.Name;
                user.Surname = usrr.Surname;
                user.Phone = usrr.Phone;
                user.Email = usrr.Email;

                return user;
            }
            else
            {
                dynamic user = new ExpandoObject();
                user.Message = "Invalid Password!";
                return user;
            }

        }
    }
}
