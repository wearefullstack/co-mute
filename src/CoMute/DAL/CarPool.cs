using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.DAL
{
    public class CarPool
    {
        public int Id { get; set; }
        public DateTime DeaprtureTime { get; set; }
        public string Origin  { get; set; }
        public int Days { get; set; }
        public string Destination { get; set; }
        public int AvailableSets { get; set; }
        public int OwnerId { get; set; }
        public string Notes { get; set; }
    }
}