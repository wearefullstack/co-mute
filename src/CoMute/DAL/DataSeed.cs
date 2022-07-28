using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.DAL
{
    public class DataSeed : System.Data.Entity.DropCreateDatabaseIfModelChanges<CoMuteContext>
    {
        //protected override void Seed(CoMuteContext context)
        //{
        //    var users= new List<Users>
        //    {
        //        new Users(){Id = 1 , Name = "Test1" , Email ="Test1!mail.com", Password = "saldjskad", Phone = "651551", Surname ="testing1"},
        //        new Users(){Id = 2 , Name = "Test2" , Email ="Test1!mail.com", Password = "saldjskad", Phone = "651551", Surname ="testing2"},
        //        new Users(){Id = 3 , Name = "Test3" , Email ="Test1!mail.com", Password = "saldjskad", Phone = "651551", Surname ="testing3"},
        //    };

        //    context.users.AddRange(users);
        //    context.SaveChanges();
        //}
    }
}