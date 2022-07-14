using co_mute_be.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace co_mute_be.Models
{

    public class CarPoolBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CarPoolBookingId { get; set; }
        public string UserId { get; set; }
        public DateTime BookedOnUtc { get; set; } = DateTime.UtcNow;
        public string CarPoolOppId { get; set; }

        //Relationships
        public User User { get; set; }
        public CarPoolOpp CarPoolOpp { get; set; }
    }


}
