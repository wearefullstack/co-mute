using CoMute.Core.Domain;
using System.Collections.Generic;

namespace CoMute.DB
{
    interface ITempDbContex
    {
        List<User> Users { get; set; }
    }
    public class TempDbContext : ITempDbContex
    {
        public TempDbContext()
        {
            Users = new List<User>();
        }
        public List<User> Users { get; set; }
    }
}