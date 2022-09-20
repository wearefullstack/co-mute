using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoMute.Web.Data.Entities
{
    public class CarPool
    {
        [Key]
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectA_Time { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Avail_Seats { get; set; }
        public string Owner_Leader { get; set; }
        public string Email { get; set; }
        public DateTime PoolCreationDate { get; set; }
        public int DaysAvailable { get; set; }
        public string Notes { get; set; }
    }
}