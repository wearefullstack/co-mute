using System.ComponentModel.DataAnnotations;

namespace comute.Models;

public class CarPool
{
    [Key]
    public int CarPoolId { get; set; }
    [Required]
    public string Origin { get; set; }
    [Required]
    public string Destination { get; set; }
    [Required]
    public DateTime DepartureTime { get; set; } = DateTime.Now;
    [Required]
    public DateTime ExpectedArrivalTime { get; set; } = DateTime.Now.AddMinutes(20);
    [Required]
    public string DaysAvailable { get; set; }
    [Required]
    public int AvailableSeats { get; set; }
    [Required]
    public int Owner { get; set; }
    public string? Notes { get; set; }
    public bool Active { get; set; } = true;
    public CarPool() { }

    public CarPool(
        int carPoolId,
        string origin,
        string destination,
        DateTime departureTime,
        DateTime expectedArrivalTime,
        string daysAvailable,
        int availableSeats,
        int owner,
        string? notes,
        bool active)
    {
        CarPoolId = carPoolId;
        Origin = origin;
        Destination = destination;
        DepartureTime = departureTime;
        ExpectedArrivalTime = expectedArrivalTime;
        DaysAvailable = daysAvailable;
        AvailableSeats = availableSeats;
        Owner = owner;
        Notes = notes;
        Active = active;
    }
}
