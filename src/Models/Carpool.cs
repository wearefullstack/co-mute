using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSWebApi.Models
{
    public class Carpool
    {
        [Key]
        public Guid CarpoolId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DayAvailable { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public string? Notes { get; set; }
        public int Seats { get; set; }    
        public Guid OwnerID { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<CarpoolMember>? Members { get; set; }



    }
}
