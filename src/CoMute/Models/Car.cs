using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
    public class Car
    { 
        public DateTime DepartureTime { get; set; }

        public DateTime ExpectedTimeArrival { get; set; }
        public string Origin { get; set; }

        public int DaysAvailable { get; set; }
        
        public string Destination { get; set; }

        public int AvailableSeats { get; set; }

        public string Owner { get; set; }

        public string Notes { get; set; }
    }
}
