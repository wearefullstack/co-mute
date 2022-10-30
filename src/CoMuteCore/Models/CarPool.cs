using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMuteCore.Models
{
    public class CarPool
    {
        public int ID { get; set; }
        public string DepartureTime { get; set; }
        public string ETA { get; set; }
        public string Origin { get; set; }
        public IEnumerable<string> DaysAvailable { get; set; }
        public int AvailableSeats { get; set; }
        public string Owner { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string Notes { get; set; }
    }
}