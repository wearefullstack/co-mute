using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models
{
    public class CarPoolJoinedByMember
    {
        public Guid CarPoolGuid { get; set; }
        public Guid UserGuid { get; set; } /*Owner*/
        public string DepartureTime { get; set; }
        public string ExpectedArrivalTime { get; set; }
        public string Origin { get; set; }
        //public string[] DaysAvailable { get; set; }
        public string Destination { get; set; }
        public int AvailableSeats { get; set; }
        public string Notes { get; set; }
        public DateTime JoindDate { get; set; }
     
        public string Status { get; set; }

        //Origin = c.Origin,
        //                              Destination = c.Destination,
        //                              DepartureTime = c.DepartureTime,
        //                              ExpectedArrivalTime = c.ExpectedArrivalTime,
        //                              AvailableSeats = c.AvailableSeats,
        //                              Notes = c.Notes,
        //                              JoindDate = mc.JoinedDate,
        //                              userGuid = mc.UserGuid,
        //                              Status = mc.Status
    }
}