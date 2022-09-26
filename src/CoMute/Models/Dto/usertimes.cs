using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    public class usertimes
    {
        public Nullable<int> Register_ID { get; set; }

        public Nullable<System.TimeSpan> Departure { get; set; }
        public Nullable<System.TimeSpan> Expected_Arrival { get; set; }
    }
}