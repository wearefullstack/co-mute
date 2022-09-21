using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class CarPoolRequest
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectArivalTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int AvailableSeats { get; set; }
        public string Owner_Leader { get; set; }
        public DateTime PoolCreationDate { get; set; }
        public int DaysAvailable { get; set; }
        public string Notes { get; set; }
    }
}