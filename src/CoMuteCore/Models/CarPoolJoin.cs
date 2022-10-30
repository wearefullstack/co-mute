using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMuteCore.Models
{
    public class CarPoolJoin
    {
        public int ID { get; set; }
        public UserCarPool UserCarPool { get; set; }
        public int UserCarPoolID { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}