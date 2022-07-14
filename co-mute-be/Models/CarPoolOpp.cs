using co_mute_be.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace co_mute_be.Models
{

    public class CarPoolOpp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CarPoolOppId { get; set; }

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public DateTime Depart { get; set; }
        public DateTime Arrive { get; set; }
        public string Origin { get; set; }

        public string Destination{ get; set; }

        //public string DaysAvailable { get; set; } //TODO : Add business logic for this
        public int AvailableSeats { get; set; }
        public string Notes { get; set; }
        
        //Relationships
        public string UserId { get; set; }
        public User User { get; set; }
        public List<CarPoolBooking> Bookings { get; set; } = new List<CarPoolBooking>();
    }


}
