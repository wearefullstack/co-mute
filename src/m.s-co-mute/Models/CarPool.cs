namespace m.s_co_mute.Models
{
    public class CarPool
    {
        public int Id { get; set; }
        public string ExpectedArrivalTime { get; set; }
        public string Origin { get; set; }
        public string DaysAvailable { get; set; }
        public string Destination { get; set; }
        public string AvailableSeats { get; set; }
        public string Owner { get; set; }
        public string? Notes { get; set; }

        public CarPool()
        {

        }
    }
}
