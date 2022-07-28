using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class AccountController : ApiController
    {
        [Route("Account/UpdateProfile")]
        public int UpdateProfile(ProfileViewModel model)
        {
            try
            {
                var context = new CoMuteEntities();
                var recored = context.Users.FirstOrDefault(x=> x.Id == model.Id);
                if (recored is null)
                    return -1;

                recored.Name = model.Name;
                recored.Surname = model.Surname;
                recored.Email = model.EmailAddress;
                recored.Phone = model.PhoneNumber;

                context.SaveChanges();

                return 1;
            }
            catch
            {
                return -1;
            }
        }

        [Route("Account/Login")]
        public int Login(LoginRequest model)
        {
            var context = new CoMuteEntities();

            try
            {
                var recored = context.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                if (recored is null)
                    return -1;

                return recored.Id;
            }
            catch
            {
                return -1;
            }
        }

        [Route("Account/GetUserProfileById")]
        public ProfileViewModel GetUserProfileById(int Id )
        {
            var context = new CoMuteEntities();

            try
            {
                var recored = context.Users.Where(x => x.Id == Id).Select(y => new ProfileViewModel
                {
                    Id = y.Id,
                    Name = y.Name,
                    Surname = y.Surname,
                    EmailAddress = y.Email,
                    PhoneNumber = y.Phone,

                }).FirstOrDefault();

                if (recored is null)
                    return new ProfileViewModel();

                return recored;
            }
            catch
            {
                return new ProfileViewModel();
            }
        }
    }
}