namespace comute.client.JoinPool;

public record JoinPoolResponse(
    int JoinId,
    int UserId,
    int CarPoolId,
    DateTime JoinedOn);
