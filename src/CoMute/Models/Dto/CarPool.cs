using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class CarPool
    {
        public int CarPoolId;

        public int UserId;

        public System.DateTime DepartureTime;

        public System.DateTime ExpectedArrivalTime;

        public string Origin;

        public int DaysAvailable;

        public string Destination;

        public int AvailableSeats;

        public string OwnerLeader;

        public string Notes;

        public IList<User> userLists { get; set; }
    }

    public class Result
    {
        public int Status
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
    }
}
