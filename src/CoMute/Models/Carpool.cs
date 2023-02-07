using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
    public class Carpool
    {
        public string departureTime { get; set; }
        public string expectedArrivalTime { get; set; }
        public string origin { get; set; }
        public string daysAvailable { get; set; }
        public string destination { get; set; }
        public string availableSeats { get; set; }
        public string ownerLeader { get; set; }
        public string notes { get; set; }
    }
}
