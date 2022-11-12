namespace comute.Models;

public class CarPoolInfo
{
    public int CarPoolId { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ExpectedArrivalTime { get; set; }
    public List<string> DaysAvailable { get; set; } = new List<string>();
    public int AvailableSeats { get; set; }
    public int Owner { get; set; }
    public User User { get; set; } = new();
    public string? Notes { get; set; }
    public bool Active { get; set; }
}
