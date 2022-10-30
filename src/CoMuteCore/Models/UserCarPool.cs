using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMuteCore.Models
{
    public class UserCarPool
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public IEnumerable<CarPool> CarPools { get; set; }
    }
}