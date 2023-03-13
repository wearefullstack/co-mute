using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
    public class CarPoolDataSetToView
    {
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Days { get; set; }
        public int Seats { get; set; }
        public TimeSpan DepartTime { get; set; }
        public TimeSpan ExpectTime { get; set; }
        public string Notes { get; set; }
    }
}