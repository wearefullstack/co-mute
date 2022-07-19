using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Services
{
    public class SecurityService
    {
        List<User> KnownUsers = new List<User>();
        public SecurityService()
        {
            KnownUsers.Add(new User {Id=0, EmailAddress="tee@gmail.com",Password="123tee" });
            KnownUsers.Add(new User {Id=1, EmailAddress="musk@gmail.com",Password="123musk" });
            KnownUsers.Add(new User {Id=2, EmailAddress="gates@gmail.com",Password="123gates" });
            KnownUsers.Add(new User {Id=3, EmailAddress="gupta@gmail.com",Password="123gupta" });
            KnownUsers.Add(new User {Id=4, EmailAddress="motsepe@gmail.com",Password="123motsepe" });
        }
        public bool IsValid(User user) 
        {
            //return true if the user is found in the list
            return KnownUsers.Any(x => x.EmailAddress == user.EmailAddress && x.Password == user.Password);
        }
    }
}