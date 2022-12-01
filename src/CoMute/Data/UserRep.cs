using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Data
{
    public class UserRep : RepBase<User>, IUserRep
    {
        public UserRep(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}