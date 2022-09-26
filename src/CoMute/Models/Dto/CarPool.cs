using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class CarPool
    {
        public int User_Car_Pool_ID { get; set; }
        public Nullable<System.TimeSpan> Departure { get; set; }
        public Nullable<System.TimeSpan> Expected_Arrival { get; set; }
        public string Origin { get; set; }
        public string Days { get; set; }
        public string Destination { get; set; }
        public Nullable<int> Available_Seats { get; set; }
        public Nullable<int> Number_Of_Passengers { get; set; }
        public string Notes { get; set; }
        public Nullable<int> Register_ID { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }

        public int Passenger_Pool_ID { get; set; }

        public Nullable<int> Status_ID { get; set; }
        public Nullable<System.DateTime> Date_Joined { get; set; }

        public int Available_Spots { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

    }
}