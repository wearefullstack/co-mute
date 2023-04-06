using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models
{
    public class Car_Pool
    {
        public int carPoolID { get; set; }  
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }

        public string origin { get; set; }

        public DateTime[] daysAvailable { get; set; }

        public string destination { get; set; }

        public int numOfAvailableSeats { get; set; }

        public string owner { get; set; }

        public string notes { get; set; }

        public static List<Car_Pool> carPools = new List<Car_Pool>();

    }
}