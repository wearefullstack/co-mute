using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.Dto
{
    //------------------------------------------- Carpool : Amber Bruil ---------------------------------------------------------//
    public class Carpool
    {
        public int CarpoolID { get; set; }
        [Required]
        public Nullable<System.TimeSpan> DepartTime { get; set; }
        [Required]
        public Nullable<System.TimeSpan> ArrivalTime { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public int DaysAvail { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public int AvailSeats { get; set; } = 0;
        [Required]
        public int UserID { get; set; }
        [Required]
        public string OwnerLeader { get; set; }
        [Required]
        public Nullable<System.DateTime> DateJoined { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        [Required]
        public string PassengerPoolID { get; set; }
        public string Notes { get; set; }
    }
    //--------------------------------------------------- 0o00ooo End of File ooo00o0 --------------------------------------------------------//

}