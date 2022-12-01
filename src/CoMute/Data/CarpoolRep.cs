using CoMute.Web.Data.DataAccess;
using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CoMute.Web.Data
{
    public class CarpoolRep : RepBase<Carpool> , ICarpoolRep
    {
        public CarpoolRep(AppDbContext appDbContext) : base(appDbContext)
        {

        }

    }
}