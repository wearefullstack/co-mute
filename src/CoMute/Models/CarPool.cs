using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models
{
    public class CarPool
    {
        [Key]
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int AvailableSeats { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AvailableDay> AvailableDays { get; set; }
        
    }
}