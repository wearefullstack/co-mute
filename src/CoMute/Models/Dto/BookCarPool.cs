using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class PassengerCarPool
    {
        public int Passenger_Pool_ID { get; set; }
        public Nullable<int> Register_ID { get; set; }
        public Nullable<int> User_Car_Pool_ID { get; set; }
        public Nullable<System.DateTime> Date_Joined { get; set; }

        public Nullable<int> Status_ID { get; set; }
    }
}