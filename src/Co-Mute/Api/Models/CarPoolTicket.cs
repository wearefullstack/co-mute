namespace Co_Mute.Api.Models
{
    public class CarPoolTicket
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public int AvailableSeats { get; set; }
        public int DaysAvailable { get; set; }
        public string? Notes { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
