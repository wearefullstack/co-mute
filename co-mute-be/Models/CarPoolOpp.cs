using co_mute_be.Abstractions.Enums;

namespace co_mute_be.Models
{

    public class CarPoolOpp
    {
        public long Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public string Origin { get; set; }
        public List<Days> DaysAvailable { get; set; }
        public int AvailableSeats { get; set; }
        public string Owner { get; set; }
        public string Notes { get; set; }
    }


}
