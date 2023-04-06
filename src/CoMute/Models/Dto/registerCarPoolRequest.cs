using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class registerCarPoolRequest
    {
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }

        public string origin { get; set; }

        public DateTime[] daysAvailable { get; set; }

        public string destination { get; set; }

        public int numOfAvailableSeats { get; set; }

        public int owner { get; set; }

        public string notes { get; set; }
    }
}