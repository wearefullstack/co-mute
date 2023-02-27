using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models
{
    [Table("CarPools")]
    public class CarPool
    {
        [Key]
        public Guid CarPoolGuid { get; set; }
        public Guid UserGuid { get; set; } /*Owner*/
        public string DepartureTime { get; set; }
        public string ExpectedArrivalTime { get; set; }
        public string Origin { get; set; }
        //public string[] DaysAvailable { get; set; }
        public string Destination { get; set; }
        public int AvailableSeats { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } 

    }
    [Table("CarPoolDays")]
    public class CarPoolDays
    {
        [Key]
        public Guid CarPoolDaysGuid { get; set; }
        public Guid CarPoolGuid { get; set; } 
        public string PoolDay { get; set; }


    }
    [Table("CarPoolMembers")]
    public class CarPoolMembers
    {
        [Key]
        public Guid CarPoolMemberGuid { get; set; }
        public Guid CarPoolGuid { get; set; }
        public Guid UserGuid { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }


    }
}