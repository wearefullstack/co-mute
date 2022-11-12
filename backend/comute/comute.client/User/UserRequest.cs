namespace comute.client.User;

public record UserRequest(
    string Name,
    string Surname,
    string? Phone,
    string Email,
    string Password,
    string Role,
    DateTime CreatedOn);