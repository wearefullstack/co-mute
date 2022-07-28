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
        public DateTime ExpectedArrivalTime { get; set; }
        public string Origin { get; set; }
        public int DaysAvailable { get; set; }
        public string Destination { get; set; }
        public int AvilableSeats { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Notes { get; set; }
    }
}