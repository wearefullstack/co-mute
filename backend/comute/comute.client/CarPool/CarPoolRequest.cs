namespace comute.client.CarPool;

public record CarPoolRequest(
    string Origin,
    string Destination,
    DateTime DepartureTime,
    DateTime ExpectedArrivalTime,
    int DaysAvailable,
    int AvailableSeats,
    int Owner,
    string? Notes,
    bool Active);
