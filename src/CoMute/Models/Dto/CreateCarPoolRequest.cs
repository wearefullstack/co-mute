using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoMute.Web.Models.Dto
{
    public class CreateCarPoolRequest
    {
        public Guid CarPoolGuid { get; set; }
        public Guid UserGuid { get; set; } /*Owner*/
        public string DepartureTime { get; set; }
        public string ExpectedArrivalTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int AvailableSeats { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}