namespace comute.client.JoinPool;

public record JoinPoolRequest(
    int UserId,
    int CarPoolId,
    DateTime JoinedOn);
