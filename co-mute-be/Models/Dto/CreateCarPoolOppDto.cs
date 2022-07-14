using co_mute_be.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace co_mute_be.Models
{

    public class CreateCarPoolOppDto
    {
        public string UserId { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Arrive { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        //public string DaysAvailable { get; set; } //TODO : Add business logic for this
        public int AvailableSeats { get; set; }
        public string Notes { get; set; }
        
    }


}
