using System.ComponentModel.DataAnnotations;

namespace Co_Mute.Api.Models.Dto
{
    public class UpdateCarPoolTicketDetailsDto
    {

        public string Origin { get; set; } = string.Empty;

        public string Destination { get; set; } = string.Empty;
  
        public string DepartureTime { get; set; } = string.Empty;
      
        public string ExpectedArrivalTime { get; set; } = string.Empty;
        
        public int AvailableSeats { get; set; }
        public string? Notes { get; set; }
    }
}
