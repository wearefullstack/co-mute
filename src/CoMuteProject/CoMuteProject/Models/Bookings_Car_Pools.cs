using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoMuteProject.Models
{
    public class Bookings_Car_Pools
    {
        public int Id { get; set; }
        public int Car_PoolId { get; set; }
        public Car_Pool Car_Pool { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime Time { get; set; }
        public string UserId { get; set; }
    }
}