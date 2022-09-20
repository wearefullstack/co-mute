using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CoMute.Web.Services
{
    public class UserService
    {
        private readonly UserManager<User> userManager;

        public UserService()
            : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> RegisterNewUser(RegistrationRequest request)
        {
            var result = await userManager.CreateAsync(new User
            {
                Email = request.EmailAddress,
                Name = request.Name,
                Surname = request.Surname,
                PhoneNumber = request.PhoneNumber,
                UserName = request.EmailAddress
            }, request.Password);

            return result.Succeeded;
        }

    }
}