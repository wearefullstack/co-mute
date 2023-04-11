namespace FSWebApi.Dto.Carpool
{
    public class CarpoolDTO
    {
        public Guid CarpoolId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateOnly DayAvailable { get; set; }
        public TimeOnly DepartureTime { get; set; }
        public TimeOnly ArrivalTime { get; set; }
        public string? Notes { get; set; }
        public int Seats { get; set; }
        public int? AvailableSeats { get; set; }  //Calc Field
        public DateTime DateCreated { get; set; }
        public Guid OwnerID { get; set; } 
        public string? OwnerDetails { get; set; } //Get from ID

        //Show Memebers?
    }
}
