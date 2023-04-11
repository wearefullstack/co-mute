using System.ComponentModel.DataAnnotations;

namespace FSWebApi.Dto.Carpool
{
    public class CreateCarpoolDTO
    {
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateOnly DayAvailable { get; set; }
        [Required]
        public TimeOnly DepartureTime { get; set; }
        [Required]
        public TimeOnly ArrivalTime { get; set; }
        public string? Notes { get; set; }
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Carpool must have 1 or more available seats.")]
        public int Seats { get; set; }
        [Required]
        public Guid OwnerID { get; set; }

    }
}
