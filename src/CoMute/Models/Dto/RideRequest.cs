
namespace CoMute.Web.Models.Dto
{
    public class RideRequest // Ride Details
    {
        public int RideId { get; set; }
        public System.DateTime DepartureTime { get; set; }
        public System.DateTime ArrivalTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int AvailableSeats { get; set; }
        public int UserId { get; set; }
        public System.DateTime Created { get; set; }
        public string Notes { get; set; }
    }
}